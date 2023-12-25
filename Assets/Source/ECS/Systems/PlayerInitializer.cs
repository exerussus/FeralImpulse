using Source.EasyECS;
using Source.ECS.Components;
using Source.ECS.Marks;
using Source.MonoBehaviours;

namespace Source.ECS.Systems
{
    public class PlayerInitializer : IEcsInitSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            
            var characterHandler = systems.GetSharedMonoBehaviour<CharactersHandler>();
            var playerEntity = _world.NewEntity();
            ref var playerData = ref _componenter.AddOrGet<CharacterData>(playerEntity);
            playerData.Value = characterHandler.PlayerCharacter;
            _componenter.Add<PlayerMark>(playerEntity);
        }
    }
}