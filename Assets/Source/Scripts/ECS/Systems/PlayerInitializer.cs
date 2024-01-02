using Source.EasyECS;
using Source.ECS.Components;
using Source.MonoBehaviours;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Marks;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
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
            var infoHub = systems.GetSharedMonoBehaviour<InfoHub>();
            
            var playerEntity = _world.NewEntity();
            ref var playerData = ref _componenter.Add<CharacterData>(playerEntity);
            playerData.Value = characterHandler.PlayerCharacter;
            _componenter.Add<PlayerMark>(playerEntity);
            _componenter.Add<FlipableMark>(playerEntity);
            
            
            ref var moveSpeedData = ref _componenter.Add<MoveSpeedData>(playerEntity);
            moveSpeedData.Value = infoHub.PlayerInfo.MoveSpeed;

            ref var rigidbodyData = ref _componenter.Add<RigidbodyData>(playerEntity);
            rigidbodyData.Value = characterHandler.PlayerCharacter.Rigidbody;
            
            ref var groundCheckData = ref _componenter.Add<GroundCheckerData>(playerEntity);
            groundCheckData.Value = characterHandler.PlayerCharacter.GroundChecker;

            ref var jumpForceData = ref _componenter.Add<JumpForceData>(playerEntity);
            jumpForceData.Value = infoHub.PlayerInfo.JumpForce;
            
            ref var spriteRenderData = ref _componenter.Add<SpriteRenderData>(playerEntity);
            spriteRenderData.Value = characterHandler.PlayerCharacter.SpriteRenderer;

            ref var leftSideTouchData = ref _componenter.Add<LeftSideCheckerData>(playerEntity);
            leftSideTouchData.Value = characterHandler.PlayerCharacter.LeftSideChecker;
            ref var rightSideTouchData = ref _componenter.Add<RightSideCheckerData>(playerEntity);
            rightSideTouchData.Value = characterHandler.PlayerCharacter.RightSideChecker;
            
            ref var animatorData = ref _componenter.Add<AnimatorData>(playerEntity);
            animatorData.Value = characterHandler.PlayerCharacter.Animator;

            ref var transformData = ref _componenter.Add<TransformData>(playerEntity);
            transformData.Value = characterHandler.PlayerCharacter.Transform;

        }
    }
}