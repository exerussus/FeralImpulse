using Source.EasyECS;
using Source.ECS.Marks;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Marks;
using Source.Scripts.ECS.Requests;
using Unity.Mathematics;
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
            _enemyLeftFilter = _world.Filter<LeftMovingMark>().Inc<FlipableMark>().Inc<TransformData>().Inc<EnemyMark>().End();
            _enemyRightFilter = _world.Filter<RightMovingMark>().Inc<FlipableMark>().Inc<TransformData>().Inc<EnemyMark>().End();
            _enemyLeftSideJumpFilter = _world.Filter<FlipJumpLeftRequest>().Inc<FlipableMark>().Inc<TransformData>().Inc<EnemyMark>().End();
            _enemyRightSideJumpFilter = _world.Filter<FlipJumpRightRequest>().Inc<FlipableMark>().Inc<TransformData>().Inc<EnemyMark>().End();
            _playerFilter = _world.Filter<PlayerMark>().Inc<FlipableMark>().Inc<TransformData>().Exc<WeaponActivatedData>().Exc<PreparingWeaponActivatedData>().Exc<AfterKickWeaponData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _enemyLeftFilter) RenderLeft(entity);
            foreach (var entity in _enemyRightFilter) RenderRight(entity);
            foreach (var entity in _enemyLeftSideJumpFilter) LeftSideJump(entity);
            foreach (var entity in _enemyRightSideJumpFilter) RightSideJump(entity);
            foreach (var entity in _playerFilter) PlayerFlip(entity);
        }

        private void SetFlip(int entity, bool value)
        {
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            var ls = transformData.Value.localScale;
            var rawScale = Mathf.Abs(ls.x);
            var scale = value ? -rawScale : rawScale;
            transformData.Value.localScale = new Vector3(scale, ls.y, ls.z);
        }

        private bool GetIsFlip(int entity)
        {
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            return transformData.Value.localScale.x < 0;
        }
        
        private void PlayerFlip(int entity)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            SetFlip(entity, mousePosition.x < transformData.Value.transform.position.x);
        }

        private void LeftSideJump(int entity)
        {
            _componenter.Del<FlipJumpLeftRequest>(entity);
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            SetFlip(entity, false);
        }
        private void RightSideJump(int entity)
        {
            _componenter.Del<FlipJumpRightRequest>(entity);
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            SetFlip(entity, true);
        }

        private void RenderRight(int entity)
        {
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            if (GetIsFlip(entity)) SetFlip(entity, false);
        }

        private void RenderLeft(int entity)
        {
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            if (!GetIsFlip(entity)) SetFlip(entity, true);
        }
    }
}