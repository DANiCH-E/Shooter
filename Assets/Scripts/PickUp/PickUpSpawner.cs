#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Shooter.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem _pickUpPrefab;

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private int _maxCount = 2;

        //[SerializeField]
        //private float _spawnIntervalSeconds = 10f;

        [SerializeField]
        private float _minSpawnIntervalSeconds = 2f;

        [SerializeField]
        private float _maxSpawnIntervalSeconds = 10f;

        private float _currentSpawnTimeSeconds;
        private int _currentCount;

        protected void Update()
        {
            if(_currentCount < _maxCount)
            {
                var spawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
                _currentSpawnTimeSeconds += Time.deltaTime;
                if(_currentSpawnTimeSeconds > spawnIntervalSeconds)
                {
                    _currentSpawnTimeSeconds = 0f;
                    _currentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

                    var pickUp = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;
                }
            }
        }

        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }
#if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
#endif
    }
}