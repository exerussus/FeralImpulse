using Source.EasyECS;
using Source.MonoBehaviours;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Marks;
using Source.Scripts.ECS.Requests;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class CombatSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _attackUpFilter;
        private EcsFilter _attackMiddleFilter;
        private EcsFilter _attackDownFilter;
        private EcsFilter _reloadFilter;
        
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _attackUpFilter = _world.Filter<AttackRequest>().Inc<MousePositionUpMark>().Exc<AttackReloadData>().End();
            _attackMiddleFilter = _world.Filter<AttackRequest>().Inc<MousePositionMiddleMark>().Exc<AttackReloadData>().End();
            _attackDownFilter = _world.Filter<AttackRequest>().Inc<MousePositionDownMark>().Exc<AttackReloadData>().End();
            _reloadFilter = _world.Filter<AttackReloadData>().End();
            
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _reloadFilter) Reload(entity);
            foreach (var entity in _attackUpFilter) AttackUp(entity);
            foreach (var entity in _attackMiddleFilter) AttackMiddle(entity);
            foreach (var entity in _attackDownFilter) AttackDown(entity);
            
        }

        private void AddAttackReload(int entity)
        {
            var timeRemaining = GetReloadTime(entity);
            ref var attackReloadData = ref _componenter.AddOrGet<AttackReloadData>(entity);
            attackReloadData.TimeRemaining = timeRemaining;
        }

        private float GetReloadTime(int entity)
        {
            TempTestCode.Start();
            var timeRemaining = 1f;
            TempTestCode.End();
            return timeRemaining;
        }
        
        private void AttackUp(int entity)
        {
            _componenter.Del<AttackRequest>(entity);
            _componenter.Add<AnimationAttackUpRequest>(entity);
            AddAttackReload(entity);
        }

        private void AttackMiddle(int entity)
        {
            _componenter.Del<AttackRequest>(entity);
            _componenter.Add<AnimationAttackMiddleRequest>(entity);
            AddAttackReload(entity);
        }

        private void AttackDown(int entity)
        {
            _componenter.Del<AttackRequest>(entity);
            _componenter.Add<AnimationAttackDownRequest>(entity);
            AddAttackReload(entity);
        }

        private void Reload(int entity)
        {
            ref var attackReloadData = ref _componenter.Get<AttackReloadData>(entity);
            attackReloadData.TimeRemaining -= Time.fixedDeltaTime;
            if (attackReloadData.TimeRemaining <= 0) _componenter.Del<AttackReloadData>(entity);
        }

        
        
    }
}