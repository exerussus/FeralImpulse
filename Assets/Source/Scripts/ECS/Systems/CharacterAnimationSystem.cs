using Source.EasyECS;
using Source.Scripts.Constants;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;
using Source.Scripts.ECS.Components.Requests.Attack;
using Source.Scripts.ECS.Components.Requests.Jump;

namespace Source.Scripts.ECS.Systems
{
    public class CharacterAnimationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private IEcsSystems _systems;
        private Componenter _componenter;
        private Filter _filter;
        private EcsFilter _idleFilter;
        private EcsFilter _runFilter;
        private EcsFilter _jumpFilter;
        private EcsFilter _attackOverheadFilter;
        private EcsFilter _attackMiddleFilter;
        private EcsFilter _attackUppercutFilter;
        private EcsFilter _groundTouchFilter;
        private EcsFilter _groundDontTouchFilter;
        private EcsFilter _sideLeftTouchFilter;
        private EcsFilter _sideRightTouchFilter;
        //private EcsFilter _sideLeftAndRightTouchFilter;
        //private EcsFilter _sideLeftAndRightDndGroundTouchFilter;

        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _systems = systems;
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _idleFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Exc<AnimationMovingMark>().End();
            _runFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<AnimationMovingMark>().End();
            _jumpFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<AnimationJumpRequest>().End();
            _attackOverheadFilter = _world.Filter<AnimatorData>().Inc<AnimationAttackUpRequest>().Inc<CharacterData>().End();
            _attackMiddleFilter = _world.Filter<AnimatorData>().Inc<AnimationAttackMiddleRequest>().Inc<CharacterData>().End();
            _attackUppercutFilter = _world.Filter<AnimatorData>().Inc<AnimationAttackDownRequestMark>().Inc<CharacterData>().End();
            _groundTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<GroundTouchMark>().End();
            _groundDontTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Exc<GroundTouchMark>().End();
            _sideLeftTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<LeftSideTouchMark>().End();
            _sideRightTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<RightSideTouchMark>().End();
            //_sideLeftAndRightTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().End();
            //_sideLeftAndRightDndGroundTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _idleFilter) Move(entity, false);
            foreach (var entity in _runFilter) Move(entity, true);
            foreach (var entity in _jumpFilter) Jump(entity);
            foreach (var entity in _attackOverheadFilter) AttackOverhead(entity);
            foreach (var entity in _attackMiddleFilter) AttackMiddle(entity);
            foreach (var entity in _attackUppercutFilter) AttackUppercut(entity);
            foreach (var entity in _groundTouchFilter) GroundTouch(entity, true);
            foreach (var entity in _groundDontTouchFilter) GroundTouch(entity, false);
            foreach (var entity in _sideLeftTouchFilter) SetSideTouch(entity, AnimationStrings.SideLeftTouch,true);
            foreach (var entity in _sideRightTouchFilter) SetSideTouch(entity, AnimationStrings.SideRightTouch,true);

        }

        private void GroundTouch(int entity, bool b)
        {
            ref var animatorData = ref _componenter.Get<AnimatorData>(entity);
            animatorData.Value.SetBool(AnimationStrings.OnGround, b);
        }

        private void SetSideTouch(int entity, string animationParameter, bool b)
        {
            ref var animatorData = ref _componenter.Get<AnimatorData>(entity);
            animatorData.Value.SetBool(animationParameter, b);
            
            
        }
        
        
        private void Move(int entity, bool isMoving)
        {
            ref var animatorData = ref _componenter.Get<AnimatorData>(entity);
            animatorData.Value.SetBool(AnimationStrings.IsMoving, isMoving);
        }

        private void Jump(int entity)
        {
            _componenter.Del<AnimationJumpRequest>(entity);
            ref var animatorData = ref _componenter.Get<AnimatorData>(entity);
            animatorData.Value.SetTrigger(AnimationStrings.Jump);
        }
        
        private void AttackOverhead(int entity)
        {
            _componenter.Del<AnimationAttackUpRequest>(entity);
            ref var animatorData = ref _componenter.Get<AnimatorData>(entity);
            animatorData.Value.SetTrigger(AnimationStrings.AttackOverhead);
        }

        private void AttackMiddle(int entity)
        {
            _componenter.Del<AnimationAttackMiddleRequest>(entity);
            ref var animatorData = ref _componenter.Get<AnimatorData>(entity);
            animatorData.Value.SetTrigger(AnimationStrings.AttackMiddle);
        }

        private void AttackUppercut(int entity)
        {
            _componenter.Del<AnimationAttackDownRequestMark>(entity);
            ref var animatorData = ref _componenter.Get<AnimatorData>(entity);
            animatorData.Value.SetTrigger(AnimationStrings.AttackUppercut);
        }
        
    }
}