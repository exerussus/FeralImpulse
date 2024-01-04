using Source.EasyECS;

namespace Source.Scripts.ECS.Systems
{
    public class HealthSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _weaponRequestsFilter;
        
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();


        }

        public void Run(IEcsSystems systems)
        {
            
            
        }
    }
}