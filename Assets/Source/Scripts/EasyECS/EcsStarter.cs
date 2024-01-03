using Source.Scripts.ECS.Systems;
using UnityEngine;

namespace Source.EasyECS
{
    public class EcsStarter : Starter
    {
        protected override void SetInitSystems(IEcsSystems initSystems)
        {
            initSystems.Add(new PlayerInitializerSystem());
        }

        protected override void SetUpdateSystems(IEcsSystems updateSystems)
        {
            updateSystems.Add(new PlayerKeysListenerSystem());
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new MovementSystem());
            fixedUpdateSystems.Add(new TouchSystem());
            fixedUpdateSystems.Add(new FlipRenderSystem());
            fixedUpdateSystems.Add(new CharacterAnimationSystem());
            fixedUpdateSystems.Add(new CombatSystem());
            fixedUpdateSystems.Add(new EntityCollidersSystem());
            
        }

        protected override void SetLateUpdateSystems(IEcsSystems lateUpdateSystems)
        {
            
        }
    }
}