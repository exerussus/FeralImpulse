
using Source.ECS.Systems;

namespace Source.EasyECS
{
    public class EcsStarter : Starter
    {
        protected override void SetInitSystems(IEcsSystems initSystems)
        {
            initSystems.Add(new PlayerInitializer());
        }

        protected override void SetUpdateSystems(IEcsSystems updateSystems)
        {
            updateSystems.Add(new KeysListenerPC());
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new MovementSystem());
        }

        protected override void SetLateUpdateSystems(IEcsSystems lateUpdateSystems)
        {
            
        }
    }
}