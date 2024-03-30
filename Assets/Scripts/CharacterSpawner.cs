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
        [SerializeField] private PlayerCharacter _playerPrefab;
        [SerializeField] private EnemyCharacter _enemyPrefab;

        private float _currentSpawnTimeSeconds;
        private int _currentCount;
        private bool _canSpawnPlayer = true;

        protected void Update()
        {
            if (_currentCount < _maxCount)
            {
                var spawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
                _currentSpawnTimeSeconds += Time.deltaTime;
                if (_currentSpawnTimeSeconds > spawnIntervalSeconds)
                {
                    _currentSpawnTimeSeconds = 0f;
                    _currentCount++;

                    int randomIndex = Random.Range(0, 2);
                    //Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
                    if (randomIndex == 0 && GameObject.FindGameObjectsWithTag("Player").Length != 0)
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
                    
                }
            }
        }

        private void OnCharacterSpawned(BaseCharacter character)
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
//using UnityEditor;
//using UnityEngine;

//namespace Shooter
//{
//    public class CharacterSpawner : MonoBehaviour
//    {
//        [SerializeField]
//        private BaseCharacter _characterPrefab;

//        [SerializeField]
//        private float _range = 2f;

//        [SerializeField]
//        private int _maxCount = 2;

//        //[SerializeField]
//        //private float _spawnIntervalSeconds = 10f;

//        [SerializeField]
//        private float _minSpawnIntervalSeconds = 2f;

//        [SerializeField]
//        private float _maxSpawnIntervalSeconds = 10f;

//        private float _currentSpawnTimeSeconds;
//        private int _currentCount;

//        protected void Update()
//        {
//            if (_currentCount < _maxCount)
//            {
//                var spawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
//                _currentSpawnTimeSeconds += Time.deltaTime;
//                if (_currentSpawnTimeSeconds > spawnIntervalSeconds)
//                {
//                    _currentSpawnTimeSeconds = 0f;
//                    _currentCount++;

//                    var randomPointInsideRange = Random.insideUnitCircle * _range;
//                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

//                    var character = Instantiate(_characterPrefab, randomPosition, Quaternion.identity, transform);
//                    Debug.Log(_characterPrefab.gameObject.name);
//                }
//            }
//        }

//        //private void OnItemPickedUp(PickUpItem pickedUpItem)
//        //{
//        //    _currentCount--;
//        //    pickedUpItem.OnPickedUp -= OnItemPickedUp;
//        //}

//        protected void OnDrawGizmos()
//        {
//            var cashedColor = Handles.color;
//            Handles.color = Color.blue;
//            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
//            Handles.color = cashedColor;
//        }
//    }
//}