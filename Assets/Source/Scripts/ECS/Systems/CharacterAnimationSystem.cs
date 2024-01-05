using Source.EasyECS;
using Source.Scripts.Constants;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Requests.Attack;
using Source.Scripts.ECS.Marks;
using Source.Scripts.ECS.Requests;
using Source.Scripts.ECS.Requests.Attack;
using Source.Scripts.test;
using UnityEngine;

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

        private int[] AnySideTouchFilter => _filter.GetEntitiesByAnyType<LeftSideTouchMark, RightSideTouchMark>();
        
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
            foreach (var entity in AnySideTouchFilter)
            {
                
            }
        }

        private void GroundTouch(int entity, bool b)
        {
            ref var animatorData = ref _componenter.Get<AnimatorData>(entity);
            animatorData.Value.SetBool(AnimationStrings.OnGround, b);
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