using Source.EasyECS;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class ExplosionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _explosionFilter;
        private EcsFilter _targetsFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _explosionFilter = _world.Filter<ExplosionData>().Inc<DeadMark>().Inc<TransformData>().End();
            _targetsFilter = _world.Filter<RigidbodyData>().Inc<TransformData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _explosionFilter) Explode(entity);
        }

        private void Explode(int entity)
        {
            ref var explosionData = ref _componenter.Get<ExplosionData>(entity);
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            foreach (var targetEntity in _targetsFilter)
            {
                ref var targetTransformData = ref _componenter.Get<TransformData>(targetEntity);
                if (transformData.Value.position.GetIsCloseDistance(targetTransformData.Value.position, explosionData.Radius))
                {
                    ref var targetRigidbodyData = ref _componenter.Get<RigidbodyData>(targetEntity);
                    targetRigidbodyData.Value.velocity +=
                       explosionData.Impulse * ((Vector2)targetTransformData.Value.position + Vector2.down - (Vector2)transformData.Value.position).normalized;
                }
            }
            _componenter.Del<ExplosionData>(entity);
        }
    }
}

///IWatcher
/// 3 поля: 1я позиция, 2я поз, пауза - движение по патрулю

///ISleepable
/// 3 поля: позиция дома, время для сна, время дл пробужждения
///
/// Смена дня и ночи. В монобехе (Easymonobeh)
/// настройки когда ночь, когда утро, TimeMultiply
