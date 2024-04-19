using Shooter.Movement;
using Shooter.PickUp;
using Shooter.Shooting;
using System;
using System.Collections;
using UnityEngine;
using System;

namespace Shooter
{

    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        public event Action<BaseCharacter> Dead;

        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _health = 2f;

        [SerializeField]
        private float _maxHealth = 6f;

        public event Action<BaseCharacter> OnSpawn;

        public float GetHP { get { return _health; } }
        public float GetMaxHP { get { return _maxHealth; } }

        private IMovementDirectionSource _movementDirectionSource;

        private CharacterMovementController _characterMovementController;
        public CharacterMovementController GetCharacterMovementController { get { return _characterMovementController; } }

        private ShootingController _shootingController;

        private bool _IsDead = false;

        private float _moveSpeed = 1f;

        private bool isMovingOppositeToTarget = false;

        public virtual void Spawn(BaseCharacter character)
        {
            OnSpawn?.Invoke(this);
        }

        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeaponPrefab);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;

            Debug.Log(direction);

            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            _animator.SetBool("IsMoving", direction != Vector3.zero);
            _animator.SetBool("IsShooting", _shootingController.HasTarget);


            if (Vector3.Dot(direction, lookDirection) < -0.5f && _shootingController.HasTarget && !isMovingOppositeToTarget)
            {
                isMovingOppositeToTarget = true;
                _animator.SetFloat("Speed", -1f);
            }
            else if (Vector3.Dot(direction, lookDirection) >= -0.5f || _shootingController.HasTarget)
            {
                isMovingOppositeToTarget = false;
                _animator.SetFloat("Speed", 1f);
            }




            if (Input.GetKeyDown(KeyCode.Space) && this is PlayerCharacter)
            {
                _characterMovementController.IncreaseSpeed();
            }
            else if (Input.GetKeyUp(KeyCode.Space) && this is PlayerCharacter)
            {
                _characterMovementController.ResetSpeed();
            }

            if (_health <= 0f)
            {
                if(!_IsDead)
                {
                    _IsDead = true;
                    _animator.SetBool("IsMoving", false);
                    _animator.SetBool("IsShooting", false);
                    _shootingController.enabled = false;
                    _characterMovementController.enabled = false;
                    StartCoroutine(Die());
                }
                
            }
              

        }

        IEnumerator Die()
        {
            _animator.SetTrigger("IsDead");
            yield return new WaitForSeconds(3f);
            Dead?.Invoke(this);
            Destroy(gameObject);
            gameObject.GetComponent<BaseCharacter>().Spawn(this);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();
                _health -= bullet.Damage;

                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpWeapon>();
                pickUp.PickUp(this);

                Destroy(other.gameObject);
            }
        }

        public Weapon GetWeapon()
        {
            return _shootingController.GetWeapon;
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }

        public float GetHPProc()
        {
            return _health / _maxHealth * 100;
        }
    }
}
