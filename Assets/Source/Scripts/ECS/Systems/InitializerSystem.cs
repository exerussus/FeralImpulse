using Source.EasyECS;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;
using Source.Scripts.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Systems
{
    public class InitializerSystem : EasySystem, IEcsInitSystem
    {
        private EcsWorld _world;
        [EasyInject] private EntityObjectHandler EntityObjectHandler;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            foreach (var monoBehaviour in EntityObjectHandler.InitializeObjects)
            {
                var entity = _world.NewEntity();
                
                if (monoBehaviour is IAnimable animable) InitAnimable(entity, animable);
                if (monoBehaviour is ICharacter character) InitCharacter(entity, character);
                if (monoBehaviour is IDestructible destructible) InitDestructible(entity, destructible);
                if (monoBehaviour is IDisplayable displayable) InitDisplayable(entity, displayable);
                if (monoBehaviour is IEnemy enemy) InitEnemy(entity, enemy);
                if (monoBehaviour is IEntityObject entityObject) InitEntityObject(entity, entityObject);
                if (monoBehaviour is IGroundChecker groundChecker) InitGroundChecker(entity, groundChecker);
                if (monoBehaviour is IHealthy healthy) InitHealth(entity, healthy);
                if (monoBehaviour is IMovable movable) InitMovable(entity, movable);
                if (monoBehaviour is IPhysicalBody physicalBody) InitPhysicalBody(entity, physicalBody);
                if (monoBehaviour is IPlayer player) InitPlayer(entity, player);
                if (monoBehaviour is ISideChecker sideChecker) InitSideChecker(entity, sideChecker);
                if (monoBehaviour is IWeaponable weaponable) InitWeaponable(entity, weaponable);
                if (monoBehaviour is ILightable lightable) InitLightable(entity, lightable);
                if (monoBehaviour is IDashable dashable) InitDashable(entity, dashable);                
                if (monoBehaviour is IStaminaExhaustable staminaExhaustable) InitStaminaExhustable(entity, staminaExhaustable);                
                if (monoBehaviour is IExplosible explosible) InitExplosible(entity, explosible);                
                if (monoBehaviour is IWatcher watcher) InitWatcher(entity, watcher);                
                if (monoBehaviour is ISleepable sleeper) InitSleeper(entity, sleeper);                
            }
        }

        private void InitAnimable(int entity, IAnimable animable)
        {
            ref var animatorData = ref Componenter.Add<AnimatorData>(entity);
            animatorData.InitializeValues(animable);
        }
        
        private void InitCharacter(int entity, ICharacter character)
        {
            ref var characterData = ref Componenter.Add<CharacterData>(entity);
            characterData.InitializeValues(character);
        }
        
        private void InitDestructible(int entity, IDestructible destructible)
        {
            Componenter.Add<DestructibleMark>(entity);
        }
        
        private void InitDisplayable(int entity, IDisplayable displayable)
        {
            ref var spriteRenderData = ref Componenter.Add<SpriteRenderData>(entity);
            spriteRenderData.InitializeValues(displayable);
        }
        
        private void InitEnemy(int entity, IEnemy enemy)
        {
            Componenter.Add<EnemyMark>(entity);
        }
        
        private void InitEntityObject(int entity, IEntityObject entityObject)
        {
            ref var entityObjectData = ref Componenter.Add<EntityObjectData>(entity);
            entityObjectData.InitializeValues(entity, entityObject);
            
            ref var transformData = ref Componenter.Add<TransformData>(entity);
            transformData.InitializeValues(entityObject);
            
            Componenter.Add<DontInitializeColliderMark>(entity);
        }
        
        private void InitGroundChecker(int entity, IGroundChecker groundChecker)
        {
            ref var groundCheckData = ref Componenter.Add<GroundCheckerData>(entity);
            groundCheckData.InitializeValues(groundChecker);
        }
        
        private void InitHealth(int entity, IHealthy healthy)
        {
            ref var healthData = ref Componenter.Add<HealthData>(entity);
            healthData.InitializeValues(healthy);
        }
        
        private void InitMovable(int entity, IMovable movable)
        {
            ref var moveSpeedData = ref Componenter.Add<MoveSpeedData>(entity);
            moveSpeedData.InitializeValues(movable);
            
            ref var jumpForceData = ref Componenter.Add<JumpForceData>(entity);
            jumpForceData.InitializeValues(movable);
            
            Componenter.Add<FlipableMark>(entity);
            Componenter.Add<LookDirectionData>(entity);
        }
        
        private void InitPhysicalBody(int entity, IPhysicalBody physicalBody)
        {
            ref var rigidbodyData = ref Componenter.Add<RigidbodyData>(entity);
            rigidbodyData.InitializeValues(physicalBody);
        }
        
        private void InitPlayer(int entity, IPlayer player)
        {
            Componenter.Add<PlayerMark>(entity);
        }
        
        private void InitSideChecker(int entity, ISideChecker sideChecker)
        {
            ref var leftSideTouchData = ref Componenter.Add<LeftSideCheckerData>(entity);
            leftSideTouchData.InitializeValues(sideChecker);
            ref var rightSideTouchData = ref Componenter.Add<RightSideCheckerData>(entity);
            rightSideTouchData.InitializeValues(sideChecker);
        }
        
        private void InitWeaponable(int entity, IWeaponable weaponable)
        {
            ref var weaponColliderHandler = ref Componenter.Add<WeaponHandlerData>(entity);
            weaponColliderHandler.InitializeValues(weaponable);
        }

        private void InitLightable(int entity, ILightable lightable)
        {
            ref var lightData = ref Componenter.Add<LightData>(entity);
            lightData.InitializeValues(lightable);
        }

        private void InitDashable(int entity, IDashable dashable)
        {            
            ref DashData dashReloadData = ref Componenter.Add<DashData>(entity);
            dashReloadData.InitializeValues(dashable);
        }
        
        private void InitStaminaExhustable(int entity, IStaminaExhaustable staminaExhaustable)
        {            
            ref StaminaData staminaData = ref Componenter.Add<StaminaData>(entity);
            staminaData.InitializeValues(staminaExhaustable);
        }

        private void InitExplosible(int entity, IExplosible explodable)
        {
            ref ExplosionData explosionData = ref Componenter.Add<ExplosionData>(entity);
            explosionData.InitializeValues(explodable);
        }
        
        private void InitWatcher(int entity, IWatcher watcher)
        {
            ref WatcherData watcherData = ref Componenter.Add<WatcherData>(entity);
            watcherData.InitializeValues(watcher);
        }
        
        private void InitSleeper(int entity, ISleepable sleeper)
        {
            ref SleepData sleepData = ref Componenter.Add<SleepData>(entity);
            sleepData.InitializeValues(sleeper);
        }
    }
}