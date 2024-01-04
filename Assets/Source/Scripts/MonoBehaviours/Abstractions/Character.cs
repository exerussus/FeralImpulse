using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public abstract class Character : MonoBehaviour, 
        ICharacter, IEntityObject, IHealthy, IPhysicalBody, IGroundChecker, 
        ISideChecker, IDisplayable, IAnimable, IWeaponable, IMovable
    {
        [SerializeField] private int entity;
        [SerializeField] private float health;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private Rigidbody2D rigidbodyValue;
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private SideChecker leftSideChecker;
        [SerializeField] private SideChecker rightSideChecker;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D entityCollider;
        [SerializeField] private WeaponColliderHandler weaponColliderHandler;
        
        public Animator Animator => animator;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Rigidbody2D Rigidbody => rigidbodyValue;
        public GroundChecker GroundChecker => groundChecker;
        public SideChecker LeftSideChecker => leftSideChecker;
        public SideChecker RightSideChecker => rightSideChecker;
        public Transform Transform => rigidbodyValue.transform;
        public WeaponColliderHandler WeaponColliderHandler => weaponColliderHandler;
        public Collider2D Collider => entityCollider;
        public int Entity => entity;
        public float Health => health;
        public float MoveSpeed => moveSpeed;
        public float JumpForce => jumpForce;

        public void InitializeEntity(int value) => entity = value;
        
    }
}