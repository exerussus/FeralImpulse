using Source.EasyECS;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;
using Source.Scripts.ECS.Components.Requests;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class StaminaSystem : IEcsInitSystem, IEcsRunSystem, IEcsSharingSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _staminaFilter;
        
        private InformationSystem _informationSystem;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();          
            _informationSystem = systems.GetSharedEcsSystem<InformationSystem>();
            _staminaFilter = _world.Filter<StaminaData>().Inc<DashData>().End(); 
            
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _staminaFilter) StaminaRun(entity);
        }

        private void StaminaRun(int entity) {
            ref var staminaData = ref _componenter.Get<StaminaData>(entity);
            ref var dashData = ref _componenter.Get<DashData>(entity);
            staminaData.CurrentValue += staminaData.Regen * Time.fixedDeltaTime;
            staminaData.CurrentValue = Mathf.Clamp(staminaData.CurrentValue, 0, staminaData.MaxValue);
            if (_componenter.Has<StaminaExhausedMark>(entity) && staminaData.CurrentValue >= dashData.StaminaPrice)
                _componenter.Del<StaminaExhausedMark>(entity);
            else if (!_componenter.Has<StaminaExhausedMark>(entity) && staminaData.CurrentValue < dashData.StaminaPrice)
                _componenter.Add<StaminaExhausedMark>(entity);
            _informationSystem.OnStaminaChange.Invoke();
        }
    }
}