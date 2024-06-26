using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Shooter.Enemy;

namespace Shooter
{
    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Loss;

        public TimerUI Timer { get; private set; }

        public CountEnemyUI CountOfEnemies { get; private set; }

        public PlayerCharacter Player { get; private set; }
        public List<EnemyCharacter> Enemies { get; private set; }

        private void Start()
        {
            Player = FindObjectOfType<PlayerCharacter>();
            Enemies = FindObjectsOfType<EnemyCharacter>().ToList();
            Timer = FindObjectOfType<TimerUI>();
            
            Player.Dead += OnPlayerDead;

            //_countEnemyUI.UpdateCountOfEnemies(Enemies.Count);

            foreach (var enemy in Enemies)
                enemy.Dead += OnEnemyDead;

            Timer.TimeEnd += PlayerLose;


        }

        private void Update()
        {
            
        }

        private void OnPlayerDead(BaseCharacter sender)
        {
            Player.Dead -= OnPlayerDead;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }

        private void OnEnemyDead(BaseCharacter sender)
        {
            var enemy = sender as EnemyCharacter;
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

