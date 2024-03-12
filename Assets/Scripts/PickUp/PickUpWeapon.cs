using Shooter.Shooting;
using UnityEngine;

namespace Shooter.PickUp
{
    public class PickUpWeapon : MonoBehaviour
    {
        [field: SerializeField]
        public Weapon WeaponPrefab { get; private set; }
       

    }
}