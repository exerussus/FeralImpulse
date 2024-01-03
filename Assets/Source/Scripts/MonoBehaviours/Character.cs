using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class Character : MonoBehaviour, IEntityObject, IHealthy
    {
        [SerializeField] private int entity;
        [SerializeField] private float health;
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

        public void InitializeEntity(int value) => entity = value;
    }
}