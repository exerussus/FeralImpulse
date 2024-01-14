using Source.EasyECS;
using Source.MonoBehaviours;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;

namespace Source.Scripts.ECS.Systems
{
    public class StealthSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _lightFilter;
        private EcsFilter _playerFilter;
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _lightFilter = _world.Filter<LightData>().Inc<TransformData>().End();
            _playerFilter = _world.Filter<PlayerMark>().Inc<TransformData>().End();

        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter) SetPlayerVisibility(entity);
        }

        private void SetPlayerVisibility(int playerEntity)
        {
            var visibility = VisibilityType.Missing;
            foreach (var lightEntity in _lightFilter)
            {
                if(_componenter.Has<PlayerMark>(lightEntity)) continue;
                var exposure = GetExposure(lightEntity);
                visibility = exposure > visibility ? exposure : visibility;
                if (visibility == VisibilityType.OnLight)
                {
                    _componenter.AddOrGet<IlluminateOnLightMark>(playerEntity);
                    _componenter.Del<IlluminateShadedMark>(playerEntity);
                    _componenter.Del<IlluminateMissingMark>(playerEntity);
                }
                else if (visibility == VisibilityType.Shaded)
                {
                    _componenter.AddOrGet<IlluminateShadedMark>(playerEntity);
                    _componenter.Del<IlluminateOnLightMark>(playerEntity);
                    _componenter.Del<IlluminateMissingMark>(playerEntity);
                }
                else
                {
                    _componenter.AddOrGet<IlluminateMissingMark>(playerEntity);
                    _componenter.Del<IlluminateOnLightMark>(playerEntity);
                    _componenter.Del<IlluminateShadedMark>(playerEntity);
                }
                
            }
            
        }

        private VisibilityType GetExposure(int lightEntity)
        {
            ref var lightData = ref _componenter.Add<LightData>(lightEntity);
            var outer = lightData.Light.pointLightOuterRadius;
            var inner = lightData.Light.pointLightInnerRadius;
            var intensity = lightData.Light.intensity;
            
            TempTestCode.Start();
            return 0;
            TempTestCode.End();
        }
        
        
    }

    public enum VisibilityType
    {
        Missing,
        Shaded,
        OnLight
    }
}