using Source.EasyECS;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;
using Source.Scripts.ECS.Components.Requests;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class HealthSystem : EasySystem, IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _weaponRequestsFilter;
        private EcsFilter _healthFilter;
        private EcsFilter _sleepDamageFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            
            _weaponRequestsFilter = _world.Filter<HealthWeaponDamageRequestData>().End();
            _healthFilter = _world.Filter<HealthData>().Exc<DeadMark>().End();
            _sleepDamageFilter = _world.Filter<HealthWeaponDamageRequestData>().Inc<ShouldSleepNowMark>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _weaponRequestsFilter) WeaponDamage(entity);
            foreach (var entity in _healthFilter) DeadCheck(entity);
            foreach (var entity in _sleepDamageFilter) SleepDamage(entity);
        }

        private void SleepDamage(int entity)
        {
            Componenter.Del<ShouldSleepNowMark>(entity);
        }

        private void WeaponDamage(int entity)
        {
            ref var weaponDamageRequest = ref _componenter.Get<HealthWeaponDamageRequestData>(entity);
            ref var weaponHandlerData = ref _componenter.Get<WeaponHandlerData>(weaponDamageRequest.OriginEntity);
            var damage = weaponHandlerData.Value.CurrentWeapon.Damage;
            foreach (var targetEntity in weaponDamageRequest.TargetEntities)
            {
                ref var healthData = ref _componenter.Get<HealthData>(targetEntity);
                healthData.CurrentValue -= damage;
                healthData.Healthy.OnHit();
            }
            _componenter.Del<HealthWeaponDamageRequestData>(entity);
        }

        private void DeadCheck(int entity)
        {
            ref var healthData = ref _componenter.Get<HealthData>(entity);
            if (healthData.CurrentValue <= 0)
            {
                _componenter.Add<DeadMark>(entity);
                healthData.Healthy.OnDead();
            }
        }

        
    }
}