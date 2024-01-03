using Source.EasyECS;
using Source.Scripts.MonoBehaviours;

namespace Source.Scripts.ECS.Systems
{
    public class DestructibleInitializerSystem : IEcsInitSystem
    {
        
        private EcsWorld _world;
        private Componenter _componenter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();

            var destructibleHandler = systems.GetSharedMonoBehaviour<DestructibleHandler>();
            

        }
    }
}