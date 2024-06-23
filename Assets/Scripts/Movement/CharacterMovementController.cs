using Shooter.Timer;
using System.Collections;
using UnityEngine;

namespace Shooter.Movement
{
    public class CharacterMovementController : IMovementController
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        private readonly ITimer _timer;

        //[SerializeField]
        //private float _initialSpeed = 5f;

        private readonly float _speed;
        //[SerializeField]
        //private float _boost = 2f;
        //[SerializeField]
        //private float _boostForEscape = 0.1f;

        private readonly float _maxRadiansDelta;
       

        //public float GetSpeed { get { return _speed; } }


        public CharacterMovementController(ICharacterConfig config, ITimer timer)
        {
            _speed = config.Speed;
            _maxRadiansDelta = config.MaxRadiansDelta;

            _timer = timer;
        }

        //public void IncreaseSpeed()
        //{
        //    _speed *= _boost;
        //}

        //public void IncreaseSpeedForEscape()
        //{
        //    _speed = _boostForEscape;
        //}

        //public void ResetSpeed()
        //{
        //    _speed = _initialSpeed;
        //}

        public Vector3 Translate(Vector3 movementDirection)
        {
            return movementDirection * _speed * _timer.DeltaTime;
        }

        public Quaternion Rotate(Quaternion currentRotation, Vector3 lookDirection)
        {
            if (_maxRadiansDelta > 0f && lookDirection != Vector3.zero)
            {
                var currentLookDirection = currentRotation * Vector3.forward;
                float sqrMagnitude = (currentLookDirection - lookDirection).sqrMagnitude;

                if (sqrMagnitude > SqrEpsilon)
                {
                    var newRotation = Quaternion.Slerp(
                        currentRotation,
                        Quaternion.LookRotation(lookDirection, Vector3.up),
                        _maxRadiansDelta * _timer.DeltaTime);

                    return newRotation;
                }
            }

            return currentRotation;
        }
    }
}