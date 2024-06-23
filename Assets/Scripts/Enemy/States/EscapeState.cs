using Shooter.FSM;
using Shooter.Movement;
using UnityEngine;

namespace Shooter.Enemy.States
{
    public class EscapeState : BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;

        private EnemyCharacterView _enemycharacter;

        private CharacterMovementController _characterMovementController;

        private Vector3 _currentPoint;

        public EscapeState(EnemyTarget target, EnemyDirectionController enemyDirectionController, EnemyCharacterView enemycharacter)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
            _enemycharacter = enemycharacter;
        }

        public override void Execute()
        {
            //_enemycharacter.GetCharacterMovementController.IncreaseSpeedForEscape();
            
            Vector3 targetPosition = _target.Closest.transform.position;

            if (_currentPoint != targetPosition)
            {
                _currentPoint = targetPosition;
                _enemyDirectionController.UpdateMovementDirection(-targetPosition);
            }
        }
    }
}
