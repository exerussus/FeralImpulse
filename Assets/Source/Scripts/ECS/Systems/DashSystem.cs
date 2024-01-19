using Source.EasyECS;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;
using Source.Scripts.ECS.Components.Requests;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class DashSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _reloadFilter;
        private EcsFilter _progressFilter;
        private EcsFilter _startFilter;
        private EcsFilter _denyFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _reloadFilter = _world.Filter<DashReloadData>().End();
            _startFilter = _world.Filter<DashRequestMark>().Exc<StaminaExhausedMark>().End();
            _denyFilter = _world.Filter<DashRequestMark>().Inc<StaminaExhausedMark>().End();
            _progressFilter = _world.Filter<RigidbodyData>().Inc<DashProgressData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _reloadFilter) DashReload(entity);
            foreach (int entity in _progressFilter) DashProgress(entity);
            foreach (int entity in _startFilter) DashStart(entity);
            foreach (int entity in _denyFilter) DashDenied(entity);
        }

        private void DashDenied(int entity)
        {
            _componenter.Del<DashRequestMark>(entity);
        }

        private void DashStart(int entity)
        {
            _componenter.Del<DashRequestMark>(entity);
            if (_componenter.Has<DashProgressData>(entity) || _componenter.Has<DashReloadData>(entity)) return;
            ref var lookDirectionData = ref _componenter.Get<LookDirectionData>(entity);
            ref var dashData = ref _componenter.Get<DashData>(entity);
            //ref var dashProgressData = ref _componenter.Add<DashProgressData>(entity);
            //dashProgressData.InitializeValues(dashData, lookDirectionData.Value);
            _componenter.AddData<DashProgressData, DashData, Vector2>(entity, dashData, lookDirectionData.Value);
            
            if (_componenter.Has<StaminaData>(entity))
            {
                ref var staminaData = ref _componenter.Get<StaminaData>(entity);
                staminaData.CurrentValue -= dashData.StaminaPrice;
            }
        }

        private void DashProgress(int entity)
        {
            ref var dashData = ref _componenter.Get<DashData>(entity);
            ref var dashProgressData = ref _componenter.Get<DashProgressData>(entity);

            dashProgressData.TimeRemaining -= Time.fixedDeltaTime;
            if (dashProgressData.TimeRemaining <= 0)
            {
                _componenter.Del<DashProgressData>(entity);
                _componenter.AddData<DashReloadData, float>(entity, dashData.Reload);
            }
            else
            {
                ref var rigidbodyData = ref _componenter.Get<RigidbodyData>(entity);
                rigidbodyData.Value.velocity = dashData.Speed * dashProgressData.Direction;
            }
        }

        private void DashReload(int entity)
        {
            ref var dashReloadData = ref _componenter.Get<DashReloadData>(entity);
            dashReloadData.TimeRemaining -= Time.fixedDeltaTime;
            if (dashReloadData.TimeRemaining <= 0)
                _componenter.Del<DashReloadData>(entity);
        }
    }
}