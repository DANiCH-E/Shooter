using Shooter.Movement;
using UnityEngine;

namespace Shooter
{
#if UNITY_ANDROID
    [RequireComponent(typeof(JoystickPlayerMovementDirectionController))]
#elif UNITY_STANDALONE || UNITY_EDITOR
    [RequireComponent(typeof(PlayerMovementDirectionController))] //��� ���������� � �����
#endif
    public class PlayerCharacter : BaseCharacter
    {
        
    }
}
