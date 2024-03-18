using UnityEngine;

namespace Shooter.Movement
{
    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera _camera;

        private CharacterMovementController _movementController;

        public Vector3 MovementDirection { get; private set; }

        protected void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _movementController = GetComponent<CharacterMovementController>();
        }

        void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector3(horizontal, 0, vertical);

            direction = _camera.transform.rotation * direction;
            direction.y = 0;

            MovementDirection = direction.normalized;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _movementController.IncreaseSpeed();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                _movementController.ResetSpeed();
            }
        }
    }
}