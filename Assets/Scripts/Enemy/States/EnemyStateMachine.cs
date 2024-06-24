using Shooter.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.Enemy.States
{
    public class EnemyStateMachine: BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5;

        private EnemyCharacterView _enemycharacter;

        public EnemyStateMachine(EnemyDirectionController enemyDirectionController,
            NavMesher navMesher, EnemyTarget target, EnemyCharacterView enemycharacter, 
            float probOfEscape, int procentForEscape)
        {
            _enemycharacter = enemycharacter;
            var idleState = new IdleState();
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var escapeState = new EscapeState(target, enemyDirectionController, enemycharacter);

            

            SetInitialState(idleState);

            AddState(state: idleState, transitions: new List<Transition>
            {
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    moveForwardState,
                    () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                 new Transition(
                    escapeState,
                    () => _enemycharacter.GetHPProc() < procentForEscape && Random.Range(0f, 100f) < probOfEscape),
            });

            AddState(state: findWayState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    moveForwardState,
                    () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
            });

            AddState(state: moveForwardState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    escapeState,
                    () => _enemycharacter.GetHPProc() < procentForEscape && Random.Range(0f, 100f) < probOfEscape),
            });

            AddState(state: escapeState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
            });
        }
    }
}
