using Source.EasyECS;
using Source.ECS.Components;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Marks;
using Source.Scripts.ECS.Requests;
using UnityEngine;
using static UnityEngine.ForceMode2D;

namespace Source.Scripts.ECS.Systems
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private IEcsSystems _systems;
        private Componenter _componenter;
        private EcsFilter _leftFilter;
        private EcsFilter _rightFilter;
        private EcsFilter _stayFilter;
        private EcsFilter _jumpSimpleFilter;
        private EcsFilter _jumpBothSideFilter;
        private EcsFilter _jumpLeftSideFilter;
        private EcsFilter _jumpRightSideFilter;

        private const float SlowdownMultiply = 0.2f;
        private const float SpeedMultiply = 0.01f;
        private const float SideJumpMultiply = 1f;
        private const float UpJumpMultiply = 1.2f;
        private Vector2 VectorUp => Vector2.up * UpJumpMultiply;
        private Vector2 VectorLeft => Vector2.left * SideJumpMultiply;
        private Vector2 VectorRight => Vector2.right * SideJumpMultiply;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _systems = systems;
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _leftFilter = _world.Filter<LeftMovingMark>().Inc<RigidbodyData>().Inc<MoveSpeedData>().Inc<GroundTouchMark>().End();
            _rightFilter = _world.Filter<RightMovingMark>().Inc<RigidbodyData>().Inc<MoveSpeedData>().Inc<GroundTouchMark>().End();
            _stayFilter = _world.Filter<CharacterData>().Inc<RigidbodyData>().Exc<LeftMovingMark>().Exc<RightMovingMark>().End();
            _jumpSimpleFilter = _world.Filter<JumpForceData>().Inc<RigidbodyData>().Inc<JumpRequest>().Inc<GroundTouchMark>().End();
            _jumpLeftSideFilter = _world.Filter<JumpForceData>().Inc<RigidbodyData>().Inc<JumpRequest>().Inc<LeftSideTouchMark>().Exc<GroundTouchMark>().End();
            _jumpRightSideFilter = _world.Filter<JumpForceData>().Inc<RigidbodyData>().Inc<JumpRequest>().Inc<RightSideTouchMark>().Exc<GroundTouchMark>().End();
            _jumpBothSideFilter = _world.Filter<JumpForceData>().Inc<RigidbodyData>().Inc<JumpRequest>().Inc<LeftSideTouchMark>().Inc<RightSideTouchMark>().Exc<GroundTouchMark>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _leftFilter) MoveLeft(entity);
            foreach (var entity in _rightFilter) MoveRight(entity);
            foreach (var entity in _stayFilter) Stay(entity);
            foreach (var entity in _jumpSimpleFilter) JumpSimple(entity);
            foreach (var entity in _jumpBothSideFilter) JumpBoth(entity);
            foreach (var entity in _jumpLeftSideFilter) JumpLeftSide(entity);
            foreach (var entity in _jumpRightSideFilter) JumpRightSide(entity);
        }

        private int GetFlipMultiply(int entity)
        {
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            return transformData.Value.localScale.x < 0 ? -1 : 1;
        }
        
        private void MoveLeft(int entity)
        {
            ref var rigidbodyData = ref _componenter.Get<RigidbodyData>(entity);
            ref var moveSpeedData = ref _componenter.Get<MoveSpeedData>(entity);

            var moveSpeed = moveSpeedData.Value * SpeedMultiply;
            
            rigidbodyData.Value.velocity = new Vector2(-moveSpeed, rigidbodyData.Value.velocity.y);
            _componenter.AddOrGet<AnimationMovingMark>(entity);
        }
        
        private void MoveRight(int entity)
        {
            ref var rigidbodyData = ref _componenter.Get<RigidbodyData>(entity);
            ref var moveSpeedData = ref _componenter.Get<MoveSpeedData>(entity);

            var moveSpeed = moveSpeedData.Value * SpeedMultiply;
            
            rigidbodyData.Value.velocity = new Vector2(moveSpeed, rigidbodyData.Value.velocity.y);
            _componenter.AddOrGet<AnimationMovingMark>(entity);
        }

        private void Stay(int entity)
        {
            ref var rigidbodyData = ref _componenter.Get<RigidbodyData>(entity);
            _componenter.Del<AnimationMovingMark>(entity);
            var velocity = rigidbodyData.Value.velocity;
            rigidbodyData.Value.velocity = new Vector2(velocity.x * SlowdownMultiply, velocity.y);
        }
        
        private void JumpSimple(int entity)
        {
            _componenter.Del<JumpRequest>(entity);
            ref var rigidbodyData = ref _componenter.Get<RigidbodyData>(entity);
            ref var jumpForceData = ref _componenter.Get<JumpForceData>(entity);
            rigidbodyData.Value.AddRelativeForce(Vector2.up * jumpForceData.Value, Impulse);
            _componenter.AddOrGet<AnimationJumpRequest>(entity);
        }
        
        private void JumpBoth(int entity)
        {
            _componenter.Del<JumpRequest>(entity);
            ref var rigidbodyData = ref _componenter.Get<RigidbodyData>(entity);
            ref var jumpForceData = ref _componenter.Get<JumpForceData>(entity);
            rigidbodyData.Value.AddRelativeForce(VectorUp * jumpForceData.Value, Impulse);
            _componenter.AddOrGet<AnimationJumpRequest>(entity);
        }
        
        private void JumpLeftSide(int entity)
        {
            _componenter.Del<JumpRequest>(entity);
            ref var rigidbodyData = ref _componenter.Get<RigidbodyData>(entity);
            ref var jumpForceData = ref _componenter.Get<JumpForceData>(entity);
            var direction = (VectorUp + VectorRight * GetFlipMultiply(entity));
            rigidbodyData.Value.AddRelativeForce(direction * jumpForceData.Value, Impulse);
            _componenter.AddOrGet<AnimationJumpRequest>(entity);
            _componenter.AddOrGet<FlipJumpLeftRequest>(entity);
        }
        
        private void JumpRightSide(int entity)
        {
            _componenter.Del<JumpRequest>(entity);
            ref var rigidbodyData = ref _componenter.Get<RigidbodyData>(entity);
            ref var jumpForceData = ref _componenter.Get<JumpForceData>(entity);
            var direction = (VectorUp + VectorLeft * GetFlipMultiply(entity));
            rigidbodyData.Value.AddRelativeForce(direction * jumpForceData.Value, Impulse);
            _componenter.AddOrGet<AnimationJumpRequest>(entity);
            _componenter.AddOrGet<FlipJumpRightRequest>(entity);
        }
    }
}