using Source.EasyECS;
using Source.ECS.Components;
using UnityEngine;

namespace Source.ECS.Systems
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private IEcsSystems _systems;
        private Componenter _componenter;
        private EcsFilter _movementFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _systems = systems;
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _movementFilter = _world.Filter<DirectionAxisData>().Inc<CharacterData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _movementFilter)
            {
                Move(entity);
            }
        }

        private void Move(int entity)
        {
            ref var characterData = ref _componenter.Get<CharacterData>(entity);
            ref var directionAxisData = ref _componenter.Get<DirectionAxisData>(entity);
            var multiply = 0.1f;
            characterData.Value.Rigidbody.velocity += new Vector2(directionAxisData.InputDirection.x * multiply, 0);
        }
    }
}