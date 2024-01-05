using Source.EasyECS;
using Source.MonoBehaviours;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Marks;
using Source.Scripts.ECS.Requests;
using Source.Scripts.ECS.Requests.Attack;
using Source.Scripts.MonoBehaviours;
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
        private EcsFilter _attackBowFilter;
        private EcsFilter _reloadFilter;
        private EcsFilter _weaponActivateFilter;
        private EcsFilter _weaponPreparingFilter;
        private EcsFilter _weaponAfterKickFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _attackUpFilter = _world.Filter<AttackRequest>().Inc<MousePositionUpMark>().Inc<WeaponHandlerData>()
                .Exc<AttackReloadData>().Exc<PreparingWeaponActivatedData>().Exc<WeaponActivatedData>()
                .Exc<AfterKickWeaponData>().End();
            _attackMiddleFilter = _world.Filter<AttackRequest>().Inc<MousePositionMiddleMark>().Inc<WeaponHandlerData>()
                .Exc<AttackReloadData>().Exc<PreparingWeaponActivatedData>().Exc<WeaponActivatedData>()
                .Exc<AfterKickWeaponData>().End();
            _attackDownFilter = _world.Filter<AttackRequest>().Inc<MousePositionDownMark>().Inc<WeaponHandlerData>()
                .Exc<AttackReloadData>().Exc<PreparingWeaponActivatedData>().Exc<WeaponActivatedData>()
                .Exc<AfterKickWeaponData>().End();
            _attackBowFilter = _world.Filter<AttackRequest>().Inc<WeaponHandlerData>().Exc<AttackReloadData>()
                .Exc<PreparingWeaponActivatedData>().Exc<WeaponActivatedData>().Exc<AfterKickWeaponData>().End();
            _reloadFilter = _world.Filter<AttackReloadData>().End();
            _weaponActivateFilter = _world.Filter<WeaponActivatedData>().Inc<WeaponHandlerData>().End();
            _weaponPreparingFilter = _world.Filter<PreparingWeaponActivatedData>().Inc<WeaponHandlerData>().End();
            _weaponAfterKickFilter = _world.Filter<AfterKickWeaponData>().Inc<WeaponHandlerData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _reloadFilter) Reload(entity);
            foreach (var entity in _attackUpFilter) AttackUp(entity);
            foreach (var entity in _attackMiddleFilter) AttackMiddle(entity);
            foreach (var entity in _attackDownFilter) AttackDown(entity);
            //foreach (var entity in _attackBowFilter) AttackBow(entity);
            foreach (var entity in _weaponActivateFilter) AttackProcess(entity);
            foreach (var entity in _weaponPreparingFilter) PrepareProcess(entity);
            foreach (var entity in _weaponAfterKickFilter) AfterKickProcess(entity);
        }

        private void AfterKickProcess(int entity)
        {
            ref var afterKickWeaponData = ref _componenter.Get<AfterKickWeaponData>(entity);
            afterKickWeaponData.TimeRemaining -= Time.fixedDeltaTime;
            if (afterKickWeaponData.TimeRemaining <= 0) _componenter.Del<AfterKickWeaponData>(entity);
        }

        private void PrepareProcess(int entity)
        {
            ref var prepareWeaponActivatedData = ref _componenter.Get<PreparingWeaponActivatedData>(entity);
            prepareWeaponActivatedData.TimeRemaining -= Time.fixedDeltaTime;
            if (prepareWeaponActivatedData.TimeRemaining <= 0)
            {
                ref var weaponHandlerData = ref _componenter.Add<WeaponHandlerData>(entity);
                ref var weaponActivatedData = ref _componenter.AddOrGet<WeaponActivatedData>(entity);
                weaponActivatedData.InitializeValues(weaponHandlerData.Value.CurrentWeapon.ActivatedDelay);
                
                weaponHandlerData.Value.Activate();
                _componenter.Del<PreparingWeaponActivatedData>(entity);
            }
        }

        private void AddAttackReload(int entity)
        {
            var timeRemaining = GetReloadTime(entity);
            ref var attackReloadData = ref _componenter.AddOrGet<AttackReloadData>(entity);
            attackReloadData.InitializeValues(timeRemaining);
        }

        private float GetReloadTime(int entity)
        {
            ref var weaponHandlerData = ref _componenter.Get<WeaponHandlerData>(entity);
            return weaponHandlerData.Value.CurrentWeapon.ReloadDelay;
        }
        
        private void AttackUp(int entity)
        {
            _componenter.Del<AttackRequest>(entity);
            _componenter.Add<AnimationAttackUpRequest>(entity);
            PrepareAttack(entity, WeaponType.Up);
            AddAttackReload(entity);

        }

        private void AttackMiddle(int entity)
        {
            _componenter.Del<AttackRequest>(entity);
            _componenter.Add<AnimationAttackMiddleRequest>(entity);
            PrepareAttack(entity, WeaponType.Middle);
            AddAttackReload(entity);
        }

        private void AttackDown(int entity)
        {
            _componenter.Del<AttackRequest>(entity);
            _componenter.Add<AnimationAttackDownRequest>(entity);
            PrepareAttack(entity, WeaponType.Down);
            AddAttackReload(entity);
        }

        private void PrepareAttack(int entity, WeaponType weaponType)
        {
            ref var weaponHandlerData = ref _componenter.Get<WeaponHandlerData>(entity);
            weaponHandlerData.Value.Prepare(weaponType);
            
            ref var prepareWeaponActivatedData = ref _componenter.Add<PreparingWeaponActivatedData>(entity);
            prepareWeaponActivatedData.InitializeValues(weaponHandlerData.Value.CurrentWeapon.PreparingDelay);
        }
        
        private void Reload(int entity)
        {
            ref var attackReloadData = ref _componenter.Get<AttackReloadData>(entity);
            attackReloadData.TimeRemaining -= Time.fixedDeltaTime;
            if (attackReloadData.TimeRemaining <= 0) _componenter.Del<AttackReloadData>(entity);
        }
        private void AttackProcess(int entity)
        {
            ref var weaponActivatedData = ref _componenter.Get<WeaponActivatedData>(entity);
            weaponActivatedData.TimeRemaining -= Time.fixedDeltaTime;
            if (weaponActivatedData.TimeRemaining <= 0)
            {
                ref var weaponHandlerData = ref _componenter.Get<WeaponHandlerData>(entity);
                weaponHandlerData.Value.Deactivate();
                ref var afterKickWeaponData = ref _componenter.Add<AfterKickWeaponData>(entity);
                afterKickWeaponData.InitializeValues(weaponHandlerData.Value.CurrentWeapon.AfterKickDelay);
                _componenter.Del<WeaponActivatedData>(entity);
            }
        }

        
        
    }
}