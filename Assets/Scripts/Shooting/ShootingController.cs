using UnityEngine;

namespace Shooter.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        private Weapon _weapon;

        private float _nextShotTimerSec;

        protected void Update()
        {
            _nextShotTimerSec -= Time.deltaTime;
            if (_nextShotTimerSec < 0)
            {
                var target = transform.forward * 100f;
                _weapon.Shoot(target);

                _nextShotTimerSec = _weapon.ShootFrequencySec;
            }
        }

        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            _weapon = Instantiate(weaponPrefab, hand);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }
    }
}