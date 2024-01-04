using Source.EasyECS;
using Source.ECS.Components;
using Source.ECS.Marks;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Marks;
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
            }
        }
        
        public void InitAnimable(int entity, IAnimable animable)
        {
            ref var animatorData = ref _componenter.Add<AnimatorData>(entity);
            animatorData.Value = animable.Animator;
        }
        
        public void InitCharacter(int entity, ICharacter character)
        {
            ref var characterData = ref _componenter.Add<CharacterData>(entity);
            characterData.Value = character;
        }
        
        public void InitDestructible(int entity, IDestructible destructible)
        {
            _componenter.Add<DestructibleMark>(entity);
        }
        
        public void InitDisplayable(int entity, IDisplayable displayable)
        {
            ref var spriteRenderData = ref _componenter.Add<SpriteRenderData>(entity);
            spriteRenderData.Value = displayable.SpriteRenderer;
        }
        
        public void InitEnemy(int entity, IEnemy enemy)
        {
            _componenter.Add<EnemyMark>(entity);
        }
        
        public void InitEntityObject(int entity, IEntityObject entityObject)
        {
            ref var entityObjectData = ref _componenter.Add<EntityObjectData>(entity);
            entityObjectData.Value = entityObject;
            entityObjectData.Value.InitializeEntity(entity);
        }
        
        public void InitGroundChecker(int entity, IGroundChecker groundChecker)
        {
            ref var groundCheckData = ref _componenter.Add<GroundCheckerData>(entity);
            groundCheckData.Value = groundChecker.GroundChecker;
        }
        
        public void InitHealth(int entity, IHealthy healthy)
        {
            ref var healthData = ref _componenter.Add<HealthData>(entity);
            healthData.InitializeValues(healthy.Health);
        }
        
        public void InitMovable(int entity, IMovable movable)
        {
            ref var moveSpeedData = ref _componenter.Add<MoveSpeedData>(entity);
            moveSpeedData.Value = movable.MoveSpeed;
            
            ref var jumpForceData = ref _componenter.Add<JumpForceData>(entity);
            jumpForceData.Value = movable.JumpForce;
            
            _componenter.Add<FlipableMark>(entity);
        }
        
        public void InitPhysicalBody(int entity, IPhysicalBody physicalBody)
        {
            ref var rigidbodyData = ref _componenter.Add<RigidbodyData>(entity);
            rigidbodyData.Value = physicalBody.Rigidbody;
            
            ref var transformData = ref _componenter.Add<TransformData>(entity);
            transformData.Value = physicalBody.Transform;
        }
        
        public void InitPlayer(int entity, IPlayer player)
        {
            _componenter.Add<PlayerMark>(entity);
        }
        
        public void InitSideChecker(int entity, ISideChecker sideChecker)
        {
            ref var leftSideTouchData = ref _componenter.Add<LeftSideCheckerData>(entity);
            leftSideTouchData.Value = sideChecker.LeftSideChecker;
            ref var rightSideTouchData = ref _componenter.Add<RightSideCheckerData>(entity);
            rightSideTouchData.Value = sideChecker.RightSideChecker;

        }
        
        public void InitWeaponable(int entity, IWeaponable weaponable)
        {
            ref var weaponColliderHandler = ref _componenter.Add<WeaponColliderHandlerData>(entity);
            weaponColliderHandler.Value = weaponable.WeaponColliderHandler;
        }
    }
}