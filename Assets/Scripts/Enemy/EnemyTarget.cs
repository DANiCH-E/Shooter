using UnityEngine;

namespace Shooter.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }

        private readonly float _viewRadius;
        private readonly Transform _agentTransform;
        private readonly PlayerCharacterView _player;
        private WeaponUtils _weaponUtils;

        private readonly Collider[] _colliders = new Collider[10];

        public EnemyTarget(Transform agent, PlayerCharacterView player, float viewRadius)
        {
            _agentTransform = agent;
            _player = player;
            _viewRadius = viewRadius;
            _weaponUtils = new WeaponUtils();
        }

        
        public void FindClosest(bool shouldChase)
        {
            float minDistance = float.MaxValue;

            var count = FindAllTargets(LayerUtils.PickUpsMask | LayerUtils.CharactersMask);

            for (int i = 0; i < count; i++)
            {
                var go = _colliders[i].gameObject;
                if (go == _agentTransform.gameObject) continue;

                if (go.gameObject.tag != _weaponUtils.Rifle && go.gameObject.tag != _weaponUtils.SniperRifle && shouldChase)
                {
                    if (_player != null && DistanceFromAgentTo(_player.gameObject) < minDistance)
                        Closest = _player.gameObject;
                    return;
                }

                var distance = DistanceFromAgentTo(go);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    Closest = go;
                }
            }


            if (_player != null && DistanceFromAgentTo(_player.gameObject) < minDistance)
                Closest = _player.gameObject;
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
                DistanceFromAgentTo(Closest);

            return 0;
        }

        private int FindAllTargets(int layerMask)
        {
            var size = Physics.OverlapSphereNonAlloc(
                _agentTransform.position,
                _viewRadius,
                _colliders,
                layerMask);

            return size;
        }

        private float DistanceFromAgentTo(GameObject go) => (_agentTransform.position - go.transform.position).magnitude;
    }
}
