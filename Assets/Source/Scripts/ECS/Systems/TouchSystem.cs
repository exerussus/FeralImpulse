using Source.EasyECS;
using Source.ECS.Components;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Marks;

namespace Source.Scripts.ECS.Systems
{
    public class TouchSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _groundCheckFilter;
        private EcsFilter _leftSideTouchFilter;
        private EcsFilter _rightSideTouchFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _groundCheckFilter = _world.Filter<GroundCheckerData>().End();
            _leftSideTouchFilter = _world.Filter<LeftSideCheckerData>().End();
            _rightSideTouchFilter = _world.Filter<RightSideCheckerData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            
            foreach (var entity in _groundCheckFilter)
            {
                TryDetectGroundTouch(entity);
            }
            foreach (var entity in _leftSideTouchFilter)
            {
                TryDetectLeftSideTouch(entity);
            }
            foreach (var entity in _rightSideTouchFilter)
            {
                TryDetectRightSideTouch(entity);
            }
        }

        private void TryDetectGroundTouch(int entity)
        {
            ref var groundCheckerData = ref _componenter.Get<GroundCheckerData>(entity);
            if (groundCheckerData.Value.IsOnGround) _componenter.AddOrGet<GroundTouchMark>(entity);
            else _componenter.Del<GroundTouchMark>(entity);
        }
        private void TryDetectLeftSideTouch(int entity)
        {
            ref var leftSideTouCheckerData = ref _componenter.Get<LeftSideCheckerData>(entity);
            if (leftSideTouCheckerData.Value.IsTouched) _componenter.AddOrGet<LeftSideTouchMark>(entity);
            else _componenter.Del<LeftSideTouchMark>(entity);
        }
        private void TryDetectRightSideTouch(int entity)
        {
            ref var rightSideTouCheckerData = ref _componenter.Get<RightSideCheckerData>(entity);
            if (rightSideTouCheckerData.Value.IsTouched) _componenter.AddOrGet<RightSideTouchMark>(entity);
            else _componenter.Del<RightSideTouchMark>(entity);
        }
        
        
    }
}