using Shooter.Movement;
using UnityEngine;

namespace Shooter
{
#if UNITY_ANDROID
    [RequireComponent(typeof(JoystickPlayerMovementDirectionController))]
#elif UNITY_STANDALONE || UNITY_EDITOR
    [RequireComponent(typeof(PlayerMovementDirectionController))] //для управления с клавы
#endif
    public class PlayerCharacter : BaseCharacter
    {
        
    }
}
