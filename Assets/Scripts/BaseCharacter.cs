using Shooter.Movement;
using Shooter.PickUp;
using Shooter.Shooting;
using UnityEngine;

namespace Shooter
{

    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private float _health = 2f;

        [SerializeField]
        private float _maxHealth = 6f;

        public float GetHP { get { return _health; } }
        public float GetMaxHP { get { return _maxHealth; } }

        private IMovementDirectionSource _movementDirectionSource;

        private CharacterMovementController _characterMovementController;
        public CharacterMovementController GetCharacterMovementController { get { return _characterMovementController; } }

        private ShootingController _shootingController;


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

            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _characterMovementController.IncreaseSpeed();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                _characterMovementController.ResetSpeed();
            }

            if (_health <= 0f)
                Destroy(gameObject);

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



        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }
    }
}
