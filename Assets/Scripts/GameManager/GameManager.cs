using System.Collections.Generic;
using UnityEngine;
using System;
using Shooter.Enemy;
using Shooter.CompositionRoot;
using Shooter.Timer;

namespace Shooter
{
    [DefaultExecutionOrder(-20)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event Action Win;
        public event Action Loss;

        [SerializeField]
        private CharacterCompositionRoot _playerCompositionRoot;

        [SerializeField]
        private List<CharacterCompositionRoot> _enemiesCompositionRoot;

        public TimerUI Timer { get; private set; }

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            ITimer timer = new UnityTimer();

            Player = (PlayerCharacterView) _playerCompositionRoot.Compose(timer);
            Enemies = new List<EnemyCharacterView>(_enemiesCompositionRoot.Count);
            foreach (var enemyRoot in _enemiesCompositionRoot)
            {
                Enemies.Add((EnemyCharacterView)enemyRoot.Compose(timer));
            }

            
            Player.Dead += OnPlayerDead;

            //_countEnemyUI.UpdateCountOfEnemies(Enemies.Count);

            foreach (var enemy in Enemies)
                enemy.Dead += OnEnemyDead;

            Timer = FindObjectOfType<TimerUI>();
            Timer.TimeEnd += PlayerLose;

            Time.timeScale = 1f;
        }



        public CountEnemyUI CountOfEnemies { get; private set; }

        public PlayerCharacterView Player { get; private set; }
        public List<EnemyCharacterView> Enemies { get; private set; }

        protected void OnDestroy()
        {
            Player.Dead -= OnPlayerDead;
            foreach (var enemy in Enemies)
                enemy.Dead -= OnEnemyDead;

            Timer.TimeEnd -= PlayerLose;
        }

        private void OnPlayerDead(BaseCharacterView sender)
        {
            Player.Dead -= OnPlayerDead;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }

        private void OnEnemyDead(BaseCharacterView sender)
        {
            var enemy = sender as EnemyCharacterView;
            Enemies.Remove(enemy);
            enemy.Dead -= OnEnemyDead;


            if (Enemies.Count == 0)
            {
                Win?.Invoke();
                Time.timeScale = 0f;
            }
        }

        private void PlayerLose()
        {
            Timer.TimeEnd -= PlayerLose;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }
    }
}

