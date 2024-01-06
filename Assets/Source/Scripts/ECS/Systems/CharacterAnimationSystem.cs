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
        private EcsFilter _idleFilter;
        private EcsFilter _runFilter;
        private EcsFilter _jumpFilter;
        private EcsFilter _attackOverheadFilter;
        private EcsFilter _attackMiddleFilter;
        private EcsFilter _attackUppercutFilter;
        private EcsFilter _groundTouchFilter;
        private EcsFilter _groundDontTouchFilter;
        
        private EcsFilter _sideBackTouchFilter;
        private EcsFilter _sideBackDontTouchFilter;
        private EcsFilter _sideFrontTouchFilter;
        private EcsFilter _sideFrontDontTouchFilter;
        

        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _systems = systems;
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            
            _idleFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Exc<AnimationMovingMark>().End();
            _runFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<AnimationMovingMark>().End();
            _jumpFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<AnimationJumpRequest>().End();
            
            _attackOverheadFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<AnimationAttackUpRequest>().End();
            _attackMiddleFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<AnimationAttackMiddleRequest>().End();
            _attackUppercutFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<AnimationAttackDownRequestMark>().End();
            
            _groundTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<GroundTouchMark>().End();
            _groundDontTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Exc<GroundTouchMark>().End();
            
            _sideBackTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<LeftSideTouchMark>().End();
            _sideBackDontTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Exc<LeftSideTouchMark>().End();
            _sideFrontTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Inc<RightSideTouchMark>().End();
            _sideFrontDontTouchFilter = _world.Filter<AnimatorData>().Inc<CharacterData>().Exc<RightSideTouchMark>().End();
            
            
        }

        public void Run(IEcsSystems systems)
        {
            _idleFilter.ForeachEntities(Move, false);
            _runFilter.ForeachEntities(Move, true);
            _jumpFilter.ForeachEntities(Jump);
            
            _attackOverheadFilter.ForeachEntities(AttackOverhead);
            _attackMiddleFilter.ForeachEntities(AttackMiddle);
            _attackUppercutFilter.ForeachEntities(AttackUppercut);
            
            _groundTouchFilter.ForeachEntities(GroundTouch, true);
            _groundDontTouchFilter.ForeachEntities(GroundTouch, false);  
                    
            _sideBackTouchFilter.ForeachEntities(SetSideTouch, AnimationStrings.SideBackTouch, true);
            _sideBackDontTouchFilter.ForeachEntities(SetSideTouch, AnimationStrings.SideBackTouch, false);
            _sideFrontTouchFilter.ForeachEntities(SetSideTouch, AnimationStrings.SideFrontTouch, true);
            _sideFrontDontTouchFilter.ForeachEntities(SetSideTouch, AnimationStrings.SideFrontTouch, false);
            
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