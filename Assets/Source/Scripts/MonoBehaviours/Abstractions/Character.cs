using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public abstract class Character : MonoBehaviour, 
        ICharacter, IEntityObject, IHealthy, IPhysicalBody, IGroundChecker, 
        ISideChecker, IDisplayable, IAnimable, IWeaponable, IMovable
    {
        [SerializeField] protected int entity;
        [SerializeField] protected float health;
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float jumpForce;
        [SerializeField] protected Rigidbody2D rigidbodyValue;
        [SerializeField] protected GroundChecker groundChecker;
        [SerializeField] protected SideChecker leftSideChecker;
        [SerializeField] protected SideChecker rightSideChecker;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Collider2D entityCollider;
        [FormerlySerializedAs("weaponColliderHandler")] [SerializeField] protected WeaponHandler weaponHandler;
        
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

        public void InitializeEntity(int value) => entity = value;
        public abstract void OnDead();
        public abstract void OnHit();

    }
}