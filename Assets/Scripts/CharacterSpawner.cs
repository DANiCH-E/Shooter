using Shooter.Enemy;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Shooter
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private float _range = 2f;
        [SerializeField] private int _maxCount = 2;
        [SerializeField] private float _minSpawnIntervalSeconds = 2f;
        [SerializeField] private float _maxSpawnIntervalSeconds = 10f;
        [SerializeField] private PlayerCharacter _playerPrefab;
        [SerializeField] private EnemyCharacter _enemyPrefab;

        private float _currentSpawnTimeSeconds;
        private int _currentCount;
        

        protected void Update()
        {
            var player = FindObjectOfType<PlayerCharacter>();
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

        private void OnCharacterSpawned(BaseCharacter character)
        {
            _currentCount--;
            character.OnSpawn -= OnCharacterSpawned;
        }
        
#if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
#endif
    }
}