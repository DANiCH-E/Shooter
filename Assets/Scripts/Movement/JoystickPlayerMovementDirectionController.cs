using UnityEngine;

namespace Shooter.Movement
{
    public class JoystickPlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera _camera;
        private Joystick _joystick;

        public Vector3 MovementDirection { get; private set; }

        protected void Awake()
        {
            _joystick = FindObjectOfType<Joystick>();
            _camera = UnityEngine.Camera.main;
        }

        void Update()
        {
            var horizontal = _joystick.Horizontal;
            var vertical = _joystick.Vertical;

            var direction = new Vector3(horizontal, 0, vertical);

            direction = _camera.transform.rotation * direction;
            direction.y = 0;

            MovementDirection = direction.normalized;

            
        }
    }
}