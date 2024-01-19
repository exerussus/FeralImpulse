using Source.EasyECS;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;
using Source.Scripts.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Systems
{
    public class InitializerSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EntityObjectHandler EntityObjectHandler { get; set; }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            EntityObjectHandler = systems.GetSharedMonoBehaviour<EntityObjectHandler>();

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
            }
        }

        public void InitAnimable(int entity, IAnimable animable)
        {
            ref var animatorData = ref _componenter.Add<AnimatorData>(entity);
            animatorData.InitializeValues(animable);
        }
        
        public void InitCharacter(int entity, ICharacter character)
        {
            ref var characterData = ref _componenter.Add<CharacterData>(entity);
            characterData.InitializeValues(character);
        }
        
        public void InitDestructible(int entity, IDestructible destructible)
        {
            _componenter.Add<DestructibleMark>(entity);
        }
        
        public void InitDisplayable(int entity, IDisplayable displayable)
        {
            ref var spriteRenderData = ref _componenter.Add<SpriteRenderData>(entity);
            spriteRenderData.InitializeValues(displayable);
        }
        
        public void InitEnemy(int entity, IEnemy enemy)
        {
            _componenter.Add<EnemyMark>(entity);
        }
        
        public void InitEntityObject(int entity, IEntityObject entityObject)
        {
            ref var entityObjectData = ref _componenter.Add<EntityObjectData>(entity);
            entityObjectData.InitializeValues(entity, entityObject);
            
            ref var transformData = ref _componenter.Add<TransformData>(entity);
            transformData.InitializeValues(entityObject);
            
            _componenter.Add<DontInitializeColliderMark>(entity);
        }
        
        public void InitGroundChecker(int entity, IGroundChecker groundChecker)
        {
            ref var groundCheckData = ref _componenter.Add<GroundCheckerData>(entity);
            groundCheckData.InitializeValues(groundChecker);
        }
        
        public void InitHealth(int entity, IHealthy healthy)
        {
            ref var healthData = ref _componenter.Add<HealthData>(entity);
            healthData.InitializeValues(healthy);
        }
        
        public void InitMovable(int entity, IMovable movable)
        {
            ref var moveSpeedData = ref _componenter.Add<MoveSpeedData>(entity);
            moveSpeedData.InitializeValues(movable);
            
            ref var jumpForceData = ref _componenter.Add<JumpForceData>(entity);
            jumpForceData.InitializeValues(movable);
            
            _componenter.Add<FlipableMark>(entity);
            _componenter.Add<LookDirectionData>(entity);
        }
        
        public void InitPhysicalBody(int entity, IPhysicalBody physicalBody)
        {
            ref var rigidbodyData = ref _componenter.Add<RigidbodyData>(entity);
            rigidbodyData.InitializeValues(physicalBody);
        }
        
        public void InitPlayer(int entity, IPlayer player)
        {
            _componenter.Add<PlayerMark>(entity);
        }
        
        public void InitSideChecker(int entity, ISideChecker sideChecker)
        {
            ref var leftSideTouchData = ref _componenter.Add<LeftSideCheckerData>(entity);
            leftSideTouchData.InitializeValues(sideChecker);
            ref var rightSideTouchData = ref _componenter.Add<RightSideCheckerData>(entity);
            rightSideTouchData.InitializeValues(sideChecker);
        }
        
        public void InitWeaponable(int entity, IWeaponable weaponable)
        {
            ref var weaponColliderHandler = ref _componenter.Add<WeaponHandlerData>(entity);
            weaponColliderHandler.InitializeValues(weaponable);
        }

        public void InitLightable(int entity, ILightable lightable)
        {
            ref var lightData = ref _componenter.Add<LightData>(entity);
            lightData.InitializeValues(lightable);
        }

        public void InitDashable(int entity, IDashable dashable)
        {            
            ref DashData dashReloadData = ref _componenter.Add<DashData>(entity);
            dashReloadData.InitializeValues(dashable);
        }
        
        public void InitStaminaExhustable(int entity, IStaminaExhaustable staminaExhaustable)
        {            
            ref StaminaData staminaData = ref _componenter.Add<StaminaData>(entity);
            staminaData.InitializeValues(staminaExhaustable);
        }

        public void InitExplosible(int entity, IExplosible explodable)
        {
            ref ExplosionData explosionData = ref _componenter.Add<ExplosionData>(entity);
            explosionData.InitializeValues(explodable);
        }
    }
}