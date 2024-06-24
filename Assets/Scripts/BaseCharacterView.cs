using Shooter.Movement;
using Shooter.PickUp;
using Shooter.Shooting;
using System;
using System.Collections;
using UnityEngine;


namespace Shooter
{

    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public abstract class BaseCharacterView : MonoBehaviour
    {
        public event Action<BaseCharacterView> Dead;

        [SerializeField]
        private WeaponFactory _baseWeapon;

        [SerializeField]
        private Transform _hand;


        private Animator _animator;
        private CharacterController _characterController;
        private CharacterMovementController _characterMovementController;

        private WeaponView _weapon;
        //[SerializeField]
        //private float _maxHealth = 6f;

        [SerializeField] HealthBarUI _healthBarUI;

        [SerializeField] private ParticleSystem _bloodDamageUI;

        [SerializeField] private ParticleSystem _deadExplosionUI;

        [SerializeField] private AudioSource _voiceDamage;

        public event Action<BaseCharacterView> OnSpawn;

        //public float GetHP { get { return _health; } }
        //public float GetMaxHP { get { return _maxHealth; } }

        private IMovementDirectionSource _movementDirectionSource;

        public BaseCharacterModel Model { get; private set; }
        //public CharacterMovementController GetCharacterMovementController { get { return _characterMovementController; } }



        private bool _IsDead = false;

        private float _moveSpeed = 1f;

        private bool isMovingOppositeToTarget = false;



        public virtual void Spawn(BaseCharacterView character)
        {
            OnSpawn?.Invoke(this);
        }

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();

            
            _healthBarUI = GetComponentInChildren<HealthBarUI>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeapon);
        }

        public void Initialize(BaseCharacterModel model)
        {
            Model = model;
            Model.Initialize(transform.position, transform.rotation);
            Model.Dead += OnDeath;
        }

        protected void Update()
        {
            Model.Move(_movementDirectionSource.MovementDirection);
            Model.TryShoot(_weapon.BulletSpawnPosition.position);

            var moveDelta = Model.Transform.Position - transform.position;
            _characterController.Move(moveDelta);
            Model.Transform.Position = transform.position;

            transform.rotation = Model.Transform.Rotation;

            _animator.SetBool("IsMoving", moveDelta != Vector3.zero);
            _animator.SetBool("IsShooting", Model.IsShooting);

            //if (Vector3.Dot(direction, lookDirection) < -0.5f && _shootingController.HasTarget && !isMovingOppositeToTarget)
            //{
            //    isMovingOppositeToTarget = true;
            //    _animator.SetFloat("Speed", -1f);
            //}
            //else if (Vector3.Dot(direction, lookDirection) >= -0.5f || _shootingController.HasTarget)
            //{
            //    isMovingOppositeToTarget = false;
            //    _animator.SetFloat("Speed", 1f);
            //}




            //if (Input.GetKeyDown(KeyCode.Space) && this is PlayerCharacterView)
            //{
            //    _characterMovementController.IncreaseSpeed();
            //}
            //else if (Input.GetKeyUp(KeyCode.Space) && this is PlayerCharacterView)
            //{
            //    _characterMovementController.ResetSpeed();
            //}

            //if (_health <= 0f)
            //{

            //    if (!_IsDead)
            //    {

            //        _IsDead = true;
            //        _animator.SetBool("IsMoving", false);
            //        _animator.SetBool("IsShooting", false);
            //        _shootingController.enabled = false;
            //        _characterMovementController.enabled = false;

            //        StartCoroutine(Die());

            //    }

            //}


        }

        //IEnumerator Die()
        //{
        //    if (_deadExplosionUI != null)
        //    {
        //        _deadExplosionUI.Play(true);
        //    }
        //    _animator.SetTrigger("IsDead");
        //    yield return new WaitForSeconds(3f);

        //    Dead?.Invoke(this);

        //    Destroy(gameObject);
        //    gameObject.GetComponent<BaseCharacter>().Spawn(this);
        //}

        protected void OnDestroy()
        {
            if (Model != null)
                Model.Dead -= OnDeath;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                _bloodDamageUI.Play();
                _voiceDamage.Play();

                Model.Damage(bullet.Damage);

                _healthBarUI.UpdateHealthBar(Model.Health, Model.MaxHP);
                Destroy(other.gameObject);
                
            }
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpWeapon>();
                pickUp.PickUp(this);

                Destroy(other.gameObject);
            }
        }

        //public WeaponView GetWeapon()
        //{
        //    return _shootingController.GetWeapon;
        //}

        public void SetWeapon(WeaponFactory weaponFactory)
        {
            if (_weapon != null)
                Destroy(_weapon.gameObject);

            _weapon = weaponFactory.Create(_hand);

            Model.SetWeapon(_weapon.Model);
        }

        public float GetHPProc()
        {
            return Model.Health / Model.MaxHP * 100;
        }

        private void OnDeath()
        {
            Dead?.Invoke(this);
            Destroy(gameObject);
        }
        //public float GetHPProc()
        //{
        //    return _health / _maxHealth * 100;
        //}
    }
}
