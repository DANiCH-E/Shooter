using Shooter.Shooting;
using UnityEngine;

namespace Shooter.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        public WeaponFactory _weaponFactory;
       
        public override void PickUp(BaseCharacterView character)
        {
            base.PickUp(character);
            character.SetWeapon(_weaponFactory);
        }
    }
}