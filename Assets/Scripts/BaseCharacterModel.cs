using Shooter.Movement;
using Shooter.Shooting;
using System;
using UnityEngine;


namespace Shooter
{

    public class BaseCharacterModel
    {
        public event Action Dead;

        public bool IsShooting => _shootingController.HasTarget;

        public TransformModel Transform { get; private set; }

        public float Health { get; private set; }


        public float MaxHP {get; private set; }

        //[SerializeField] HealthBarUI _healthBarUI;

        //[SerializeField] private ParticleSystem _bloodDamageUI;

        //[SerializeField] private ParticleSystem _deadExplosionUI;

        //[SerializeField] private AudioSource _voiceDamage;

        //public event Action<BaseCharacter> OnSpawn;

        //public float GetHP { get { return _health; } }
        //public float GetMaxHP { get { return _maxHealth; } }


        private readonly IMovementController _characterMovementController;
        //public CharacterMovementController GetCharacterMovementController { get { return _characterMovementController; } }

        private readonly ShootingController _shootingController;

        private bool _IsDead = false;

        private float _moveSpeed = 1f;

        private bool isMovingOppositeToTarget = false;



        //public virtual void Spawn(BaseCharacter character)
        //{
        //    OnSpawn?.Invoke(this);
        //}

        public BaseCharacterModel(IMovementController movementController, 
            ShootingController shootingController, ICharacterConfig config)
        {
            _characterMovementController = movementController;
            _shootingController = shootingController;
            Health = config.Health;
        }

        public void Initialize(Vector3 position, Quaternion rotation)
        {
            Transform = new TransformModel(position, rotation);
        }

        public void Move(Vector3 direction)
        {
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - Transform.Position).normalized;

            Transform.Position += _characterMovementController.Translate(direction);
            Transform.Rotation = _characterMovementController.Rotate(Transform.Rotation, lookDirection);
        }

        public void Damage(float damage)
        {
            Health -= damage;

            if (Health <= 0f)
                Dead?.Invoke();
            
        }

        public void TryShoot(Vector3 shotPosition)
        {
            _shootingController.TryShoot(shotPosition);
        }

        //protected void Update()
        //{
        //    var direction = _movementDirectionSource.MovementDirection;
        //    var lookDirection = direction;
        //    if (_shootingController.HasTarget)
        //        lookDirection = (_shootingController.TargetPosition - transform.position).normalized;

        //    _characterMovementController.MovementDirection = direction;
        //    _characterMovementController.LookDirection = lookDirection;

        //    //_animator.SetBool("IsMoving", direction != Vector3.zero);
        //    //_animator.SetBool("IsShooting", _shootingController.HasTarget);





        //    if (Input.GetKeyDown(KeyCode.Space) && this is PlayerCharacter)
        //    {
        //        _characterMovementController.IncreaseSpeed();
        //    }
        //    else if (Input.GetKeyUp(KeyCode.Space) && this is PlayerCharacter)
        //    {
        //        _characterMovementController.ResetSpeed();
        //    }

        //    if (Health <= 0f)
        //    {

        //        Dead?.Invoke(this);
        //        Destroy(gameObject)
                
        //    }
              

        //}

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


        //public Weapon GetWeapon()
        //{
        //    return _shootingController.GetWeapon;
        //}

        public void SetWeapon(WeaponModel weapon)
        {
            _shootingController.SetWeapon(weapon);
        }

        
    }
}
