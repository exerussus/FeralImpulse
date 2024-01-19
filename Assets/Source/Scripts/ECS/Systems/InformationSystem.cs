using System;
using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;

namespace Source.Scripts.ECS.Systems
{
    public class InformationSystem : EasySystem, IEcsInitSystem, IEcsSharingSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        
        private EcsFilter _playerfilter;

        public Action OnHealthChange;
        public Action OnStaminaChange;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();

            _playerfilter = _world.Filter<PlayerMark>().End();
            
        }

        public T GetPlayerData<T>() where T : struct, IEcsComponent
        {
            return _componenter.Get<T>(_playerfilter.GetFirstEntity());
        }
        
        public T GetData<T>(int entity) where T : struct, IEcsComponent
        {
            return _componenter.Get<T>(entity);
        }

        public float GetPlayerCurrentHealth()
        {
            return _componenter.Get<HealthData>(_playerfilter.GetFirstEntity()).CurrentValue;
        }
        
        public float GetPlayerMaxHealth()
        {
            return _componenter.Get<HealthData>(_playerfilter.GetFirstEntity()).MaxValue;
        }
        
        public float GetCharacterCurrentHealth(int entity)
        {
            return _componenter.Get<HealthData>(entity).CurrentValue;
        }
        
        public float GetCharacterMaxHealth(int entity)
        {
            return _componenter.Get<HealthData>(entity).MaxValue;
        }

        public float GetMaxStamina()
        {
            return _componenter.Get<StaminaData>(_playerfilter.GetFirstEntity()).MaxValue;
        }
        
        public float GetCurrentStamina()
        {
            return _componenter.Get<StaminaData>(_playerfilter.GetFirstEntity()).CurrentValue;
        }

    }
}