using Shooter.Timer;
using UnityEngine;

namespace Shooter.Shooting
{
    public class ShootingController
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.Transform.Position;

        private WeaponModel _weapon;

        private readonly IShootingTarget _shootingTarget;
        private readonly ITimer _timer;

        private BaseCharacterModel _target;
        private float _nextShotTimerSec;

        public ShootingController(IShootingTarget shootingTarget, ITimer timer)
        {
            _timer = timer;
            _shootingTarget = shootingTarget;
        }

        public void TryShoot(Vector3 position)
        {
            _target = _shootingTarget.GetTarget(position, _weapon.Description.ShootRadius);

            _nextShotTimerSec -= _timer.DeltaTime;
            if (_nextShotTimerSec < 0)
            {
                if (HasTarget)
                    _weapon.Shoot(position, TargetPosition);

                _nextShotTimerSec = _weapon.Description.ShootFrequencySec;
            }
        }

        //public Weapon GetWeapon { get { return _weapon; } }

        public void SetWeapon(WeaponModel weapon)
        {
            _weapon = weapon;
        }
    }
}