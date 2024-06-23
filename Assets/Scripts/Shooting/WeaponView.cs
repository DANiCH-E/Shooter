﻿
using UnityEngine;

namespace Shooter.Shooting
{
    public class WeaponView : MonoBehaviour
    {
        [field: SerializeField]
        public Transform BulletSpawnPosition { get; private set; }

        [SerializeField]
        private Bullet _bulletPrefab;

        [SerializeField]
        private ParticleSystem _shootingParticle;

        [SerializeField]
        private AudioSource _shootAudio;

        public WeaponModel Model { get; private set; }

        public void Initialize(WeaponModel model)
        {
            if (Model != null)
            {
                Debug.LogWarning("Weapon model has been already initialized!");
                return;
            }

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            Model = model;
            Model.Shot += Shoot;
        }

        protected void OnDestroy()
        {
            if (Model != null)
                Model.Shot -= Shoot;
        }

        public void Shoot(Vector3 targetDirection, WeaponDescription description)
        {
            var bullet = Instantiate(_bulletPrefab, BulletSpawnPosition.position, Quaternion.identity);
            bullet.Initialize(targetDirection, description.BulletMaxFlyDistance, 
                description.BulletFlySpeed, description.Damage);

            _shootingParticle.Play();
            _shootAudio.Play();

            
        }

    }
}