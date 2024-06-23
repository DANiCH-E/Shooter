
using UnityEngine;

namespace Shooter.Shooting
{
    public interface IShootingTarget
    {
        BaseCharacterModel GetTarget(Vector3 position, float radius);
    }
}
