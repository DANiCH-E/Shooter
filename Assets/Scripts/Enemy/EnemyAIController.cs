using Shooter.Enemy;
using Shooter.Enemy.States;
using Shooter.Shooting;
using System.Collections;
using UnityEngine;

namespace Shooter.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] private float _viewRadius = 20f;
        [SerializeField] private float _probOfEscape = 70f;
        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;
        private EnemyCharacter _enemycharacter;

        private bool shouldChase = false;
        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            _enemycharacter = GetComponent<EnemyCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();

            var navMesher = new NavMesher(transform);
            _target = new EnemyTarget(transform, player, _viewRadius);

            

            _stateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, _target, _enemycharacter, _probOfEscape);
        }
        protected void Update()
        {
            Weapon weapon = _enemycharacter.GetWeapon();
            if (weapon.tag != "Pistol")
                shouldChase = true;
            //Debug.Log(_enemycharacter);
            //Debug.Log(weapon.gameObject.name);
            //var hp = _enemycharacter.GetHP;
            //Debug.Log(hp);
            _target.FindClosest(shouldChase);
            _stateMachine.Update();
        }
    }
}