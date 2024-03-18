using Shooter.Shooting;
using UnityEngine;

namespace Shooter.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        public Weapon _weaponPrefab;
       
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(_weaponPrefab);
        }
    }
}