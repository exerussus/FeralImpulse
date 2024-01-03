using Source.EasyECS;
using Source.Scripts.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Systems
{
    public class InitializerSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EntityObjectHandler _entityObjectHandler;
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _entityObjectHandler = systems.GetSharedMonoBehaviour<EntityObjectHandler>();

            foreach (var monoBehaviour in _entityObjectHandler.InitializeObjects)
            {
                var entity = _world.NewEntity();
                
                if (monoBehaviour is IEntityObject entityObject) InitEntityObject(entity, entityObject);
                if (monoBehaviour is ICharacter character) InitCharacter(entity, character);
                if (monoBehaviour is IDestructible destructible) InitDestructible(entity, destructible);
                if (monoBehaviour is IHealthy healthy) InitHealth(entity, healthy);
                if (monoBehaviour is IAnimable animable) InitAnimable(entity, animable);
                
            }
        }

        public void InitCharacter(int entity, ICharacter character)
        {
            
        }

        public void InitDestructible(int entity, IDestructible destructible)
        {
            
        }

        public void InitEntityObject(int entity, IEntityObject entityObject)
        {
            
        }

        public void InitHealth(int entity, IHealthy healthy)
        {
            
        }

        public void InitAnimable(int entity, IAnimable animable)
        {
            
        }
    }
}