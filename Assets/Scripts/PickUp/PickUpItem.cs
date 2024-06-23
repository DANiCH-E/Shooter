using Shooter.Shooting;
using System;
using UnityEngine;

namespace Shooter.PickUp
{
    public abstract class PickUpItem : MonoBehaviour
    {
        public event Action<PickUpItem> OnPickedUp;

        public virtual void PickUp(BaseCharacterView character)
        {
            OnPickedUp?.Invoke(this);
        }

    }
}