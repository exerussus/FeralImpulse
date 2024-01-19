using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Персонаж.
    /// </summary>
    
    public abstract class Character : MonoBehaviour, 
        ICharacter, IEntityObject, IHealthy, IPhysicalBody, IGroundChecker, 
        ISideChecker, IDisplayable, IAnimable, IWeaponable, IMovable, 
        IDashable, IStaminaExhaustable
    {
        [SerializeField] protected int entity;
        [SerializeField] protected float health;
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float jumpForce;
        [SerializeField] protected float dashSpeed;
        [SerializeField] protected float dashTime;
        [SerializeField] protected float dashReload;
        [SerializeField] protected float dashStaminaPrice;
        [SerializeField] protected float staminaMax;
        [SerializeField] protected float staminaRegen;
        [SerializeField] protected Rigidbody2D rigidbodyValue;
        [SerializeField] protected GroundChecker groundChecker;
        [SerializeField] protected SideChecker leftSideChecker;
        [SerializeField] protected SideChecker rightSideChecker;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Collider2D entityCollider;
        [SerializeField] protected WeaponHandler weaponHandler;
        
        public Animator Animator => animator;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Rigidbody2D Rigidbody => rigidbodyValue;
        public GroundChecker GroundChecker => groundChecker;
        public SideChecker LeftSideChecker => leftSideChecker;
        public SideChecker RightSideChecker => rightSideChecker;
        public Transform Transform => rigidbodyValue.transform;
        public WeaponHandler WeaponHandler => weaponHandler;
        public Collider2D Collider => entityCollider;
        public int Entity => entity;
        public float Health => health;
        public float MoveSpeed => moveSpeed;
        public float JumpForce => jumpForce;
        public float DashSpeed => dashSpeed;
        public float DashTime => dashTime;
        public float Reload => dashReload;
        public float StaminaPrice => dashStaminaPrice;
        public float Stamina => staminaMax;
        public float Regen => staminaRegen;
        public void InitializeEntity(int value) => entity = value;
        public abstract void OnDead();
        public abstract void OnHit();

    }
}