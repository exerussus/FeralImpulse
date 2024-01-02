using Source.EasyECS;
using Source.ECS.Components;
using Source.ECS.Marks;
using Source.Scripts.ECS.Marks;
using Source.Scripts.ECS.Requests;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class FlipRenderSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _enemyLeftFilter;
        private EcsFilter _enemyRightFilter;
        private EcsFilter _enemyLeftSideJumpFilter;
        private EcsFilter _enemyRightSideJumpFilter;
        private EcsFilter _playerFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _enemyLeftFilter = _world.Filter<LeftMovingMark>().Inc<FlipableMark>().Inc<SpriteRenderData>().Inc<EnemyMark>().End();
            _enemyRightFilter = _world.Filter<RightMovingMark>().Inc<FlipableMark>().Inc<SpriteRenderData>().Inc<EnemyMark>().End();
            _enemyLeftSideJumpFilter = _world.Filter<FlipJumpLeftRequest>().Inc<FlipableMark>().Inc<SpriteRenderData>().Inc<EnemyMark>().End();
            _enemyRightSideJumpFilter = _world.Filter<FlipJumpRightRequest>().Inc<FlipableMark>().Inc<SpriteRenderData>().Inc<EnemyMark>().End();
            _playerFilter = _world.Filter<PlayerMark>().Inc<FlipableMark>().Inc<SpriteRenderData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _enemyLeftFilter) RenderLeft(entity);
            foreach (var entity in _enemyRightFilter) RenderRight(entity);
            foreach (var entity in _enemyLeftSideJumpFilter) LeftSideJump(entity);
            foreach (var entity in _enemyRightSideJumpFilter) RightSideJump(entity);
            foreach (var entity in _playerFilter) PlayerFlip(entity);
        }

        private void PlayerFlip(int entity)
        {
            ref var spriteRenderData = ref _componenter.Get<SpriteRenderData>(entity);
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spriteRenderData.Value.flipX = mousePosition.x < spriteRenderData.Value.transform.position.x;

        }

        private void LeftSideJump(int entity)
        {
            _componenter.Del<FlipJumpLeftRequest>(entity);
            ref var spriteRenderData = ref _componenter.Get<SpriteRenderData>(entity);
            spriteRenderData.Value.flipX = false;
        }
        private void RightSideJump(int entity)
        {
            _componenter.Del<FlipJumpRightRequest>(entity);
            ref var spriteRenderData = ref _componenter.Get<SpriteRenderData>(entity);
            spriteRenderData.Value.flipX = true;
        }

        private void RenderRight(int entity)
        {
            ref var spriteRenderData = ref _componenter.Get<SpriteRenderData>(entity);
            if (spriteRenderData.Value.flipX) spriteRenderData.Value.flipX = false;
        }

        private void RenderLeft(int entity)
        {
            ref var spriteRenderData = ref _componenter.Get<SpriteRenderData>(entity);
            if (!spriteRenderData.Value.flipX) spriteRenderData.Value.flipX = true;
        }
    }
}