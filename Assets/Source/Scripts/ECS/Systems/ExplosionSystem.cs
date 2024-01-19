using Source.EasyECS;
using Source.MonoBehaviours;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class ExplosionSystem : EasySystem, IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _explosionFilter;
        private EcsFilter _targetsFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _explosionFilter = _world.Filter<ExplosionData>().Inc<DeadMark>().Inc<TransformData>().End();
            _targetsFilter = _world.Filter<RigidbodyData>().Inc<TransformData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _explosionFilter) Explode(entity);
        }

        private void Explode(int entity)
        {
            ref var explosionData = ref Componenter.Get<ExplosionData>(entity);
            ref var transformData = ref Componenter.Get<TransformData>(entity);
            foreach (var targetEntity in _targetsFilter)
            {
                ref var targetTransformData = ref Componenter.Get<TransformData>(targetEntity);
                if (transformData.Value.position.GetIsCloseDistance(targetTransformData.Value.position, explosionData.Radius))
                {
                    ref var targetRigidbodyData = ref Componenter.Get<RigidbodyData>(targetEntity);
                    targetRigidbodyData.Value.velocity +=
                       explosionData.Impulse * ((Vector2)targetTransformData.Value.position + Vector2.down - (Vector2)transformData.Value.position).normalized;
                }
            }
            Componenter.Del<ExplosionData>(entity);
        }
    }
}
