using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public abstract class Destructible : MonoBehaviour, IEntityObject, IDestructible, IDisplayable, IAnimable, IHealthy
    {
        [SerializeField] protected int entity;
        [SerializeField] protected float health;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Collider2D entityCollider;
        
        public Animator Animator => animator;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Collider2D Collider => entityCollider;
        public int Entity => entity;
        public float Health => health;
        
        public void InitializeEntity(int value) => entity = value;

        public abstract void OnDead();
    }
}