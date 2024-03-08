﻿using System.Collections;
using UnityEngine;

namespace Shooter.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        [SerializeField]
        private float _initialSpeed = 5f;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _boost = 2f;
        [SerializeField]
        private float _maxRadiansDelta = 10f;
        

        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;

        protected void Awake()
        {
            _speed = _initialSpeed;
            _characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();

            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
                Rotate();
        }

        private void Translate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _speed *= _boost;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                _speed = _initialSpeed;
            }

            var delta = MovementDirection * _speed * Time.deltaTime;
            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;

            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadiansDelta * Time.deltaTime);

                transform.rotation = newRotation;
            }
        }
    }
}