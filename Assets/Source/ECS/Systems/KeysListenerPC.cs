using Source.EasyECS;
using Source.ECS.Components;
using Source.ECS.Marks;
using UnityEngine;

namespace Source.ECS.Systems
{
    public class KeysListenerPC : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private IEcsSystems _systems;
        private Componenter _componenter;
        private EcsFilter _playerFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _systems = systems;
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _playerFilter = _world.Filter<PlayerMark>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter)
            {
                TryAddDirectionAxisData(entity);
            }
        }

        private void TryAddDirectionAxisData(int entity)
        {
            var axisX = Input.GetAxis("Horizontal");
            var axisY = Input.GetAxis("Vertical");

            if (axisX == 0 && axisY == 0)
            {
                _componenter.Del<DirectionAxisData>(entity);
                return;
            }
            ref var inputAxisData = ref _componenter.AddOrGet<DirectionAxisData>(entity);
            inputAxisData.InputDirection = new Vector2(axisX, axisY);
        }
    }
}