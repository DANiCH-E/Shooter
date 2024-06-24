using Shooter.Enemy;
using UnityEditor;
using UnityEngine;

namespace Shooter
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private float _range = 2f;
        [SerializeField] private int _maxCount = 2;
        [SerializeField] private float _minSpawnIntervalSeconds = 2f;
        [SerializeField] private float _maxSpawnIntervalSeconds = 10f;
        [SerializeField] private PlayerCharacterView _playerPrefab;
        [SerializeField] private EnemyCharacterView _enemyPrefab;

        private float _currentSpawnTimeSeconds;
        private int _currentCount;
        

        protected void Update()
        {
            var player = FindObjectOfType<PlayerCharacterView>();
            if (_currentCount < _maxCount)
            {
                var spawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
                _currentSpawnTimeSeconds += Time.deltaTime;
                if (_currentSpawnTimeSeconds > spawnIntervalSeconds)
                {
                    _currentSpawnTimeSeconds = 0f;
                    _currentCount++;

                    int randomIndex = Random.Range(0, 2);
                    
                    if (randomIndex == 0 && !player)
                    {
                        var randomPointInsideRange = Random.insideUnitCircle * _range;
                        var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;
                        var character = Instantiate(_playerPrefab, randomPosition, Quaternion.identity, transform);
                        
                        character.OnSpawn += OnCharacterSpawned;
                    }
                    else
                    {
                        var randomPointInsideRange = Random.insideUnitCircle * _range;
                        var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;
                        var character = Instantiate(_enemyPrefab, randomPosition, Quaternion.identity, transform);
                        
                        character.OnSpawn += OnCharacterSpawned;
                    }
                    //Debug.Log(_currentCount);
                    
                }
            }
        }

        private void OnCharacterSpawned(BaseCharacterView character)
        {
            _currentCount--;
            character.OnSpawn -= OnCharacterSpawned;
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
    }
}