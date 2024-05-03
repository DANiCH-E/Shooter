using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EnemyPointer : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Camera _camera;
    [SerializeField] Transform _pointerIconTransform;

    protected void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 fromPlayerToEnemy = transform.position - _playerTransform.position;
        Ray ray = new Ray(_playerTransform.position, fromPlayerToEnemy);
        Debug.DrawRay(_playerTransform.position, fromPlayerToEnemy);

        //[0] = Left, [1] = Right, [2] = Down, [3] = Up
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        float minDistance = Mathf.Infinity;

        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance)) {
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
        }

        minDistance = Mathf.Clamp(minDistance, 0, fromPlayerToEnemy.magnitude);
        Vector3 worldPosition = ray.GetPoint(minDistance);
        _pointerIconTransform.position = _camera.WorldToScreenPoint(worldPosition);
    }
}
