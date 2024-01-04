using Source.EasyECS;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Marks;
using Source.Scripts.ECS.Requests;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class PlayerKeysListenerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private IEcsSystems _systems;
        private Componenter _componenter;
        private EcsFilter _playerMoveFilter;
        private EcsFilter _jumpGroundFilter;
        private EcsFilter _jumpLeftTouchFilter;
        private EcsFilter _jumpRightTouchFilter;
        private EcsFilter _jumpBothFilter;
        private EcsFilter _mousePositionFilter;
        private EcsFilter _attackFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _systems = systems;
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _playerMoveFilter = _world.Filter<PlayerMark>().Inc<GroundTouchMark>().End();
            _jumpGroundFilter = _world.Filter<PlayerMark>().Inc<GroundTouchMark>().End();
            _jumpLeftTouchFilter = _world.Filter<PlayerMark>().Inc<LeftSideTouchMark>().Exc<GroundTouchMark>().End();
            _jumpRightTouchFilter = _world.Filter<PlayerMark>().Inc<RightSideTouchMark>().Exc<GroundTouchMark>().End();
            _jumpBothFilter = _world.Filter<PlayerMark>().Inc<LeftSideTouchMark>().Inc<RightSideTouchMark>().Exc<GroundTouchMark>().End();
            _mousePositionFilter = _world.Filter<PlayerMark>().Inc<TransformData>().End();
            _attackFilter = _world.Filter<PlayerMark>().Exc<AttackReloadData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerMoveFilter) TryAddMovingDirection(entity);
            foreach (var entity in _jumpGroundFilter) TryCreateJumpRequest(entity);
            foreach (var entity in _jumpLeftTouchFilter) TryCreateJumpRequest(entity);
            foreach (var entity in _jumpRightTouchFilter) TryCreateJumpRequest(entity);
            foreach (var entity in _jumpBothFilter) TryCreateJumpRequest(entity);
            foreach (var entity in _mousePositionFilter) TryAddMousePosition(entity);
            foreach (var entity in _attackFilter) TryAttack(entity);
        }

        private void TryAttack(int entity)
        {
            if (Input.GetMouseButtonDown(0))_componenter.AddOrGet<AttackRequest>(entity);
        }

        private void TryAddMousePosition(int entity)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ref var transformData = ref _componenter.Get<TransformData>(entity);
            
            var mousePositionY = mousePosition.y;
            var characterPositionY = transformData.Value.position.y;
            var upLine = 1f;
            var downLine = -1f;

            if (mousePositionY > characterPositionY + upLine) 
            {
                _componenter.AddOrGet<MousePositionUpMark>(entity);
                _componenter.Del<MousePositionMiddleMark>(entity);
                _componenter.Del<MousePositionDownMark>(entity);
            }
            else if (mousePositionY < characterPositionY + downLine)
            {
                _componenter.AddOrGet<MousePositionDownMark>(entity);
                _componenter.Del<MousePositionMiddleMark>(entity);
                _componenter.Del<MousePositionUpMark>(entity);
            }
            else
            {
                _componenter.AddOrGet<MousePositionMiddleMark>(entity);
                _componenter.Del<MousePositionDownMark>(entity);
                _componenter.Del<MousePositionUpMark>(entity);
            }

        }

        private void TryAddMovingDirection(int entity)
        {
            var pressedLeft = Input.GetKey(KeyCode.A);
            var pressedRight = Input.GetKey(KeyCode.D);

            if (pressedLeft && pressedRight || !pressedRight && !pressedLeft)
            {
                _componenter.Del<LeftMovingMark>(entity);
                _componenter.Del<RightMovingMark>(entity);
                return;
            }

            if (pressedLeft)
            {
                _componenter.AddOrGet<LeftMovingMark>(entity);
                _componenter.Del<RightMovingMark>(entity);
                return;
            }

            _componenter.AddOrGet<RightMovingMark>(entity);
            _componenter.Del<LeftMovingMark>(entity);
        }

        private void TryCreateJumpRequest(int entity)
        {
            if (Input.GetKeyDown(KeyCode.Space)) _componenter.AddOrGet<JumpRequest>(entity);
        }
        
        
    }
}