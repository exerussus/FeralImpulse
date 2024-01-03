using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class Destructible : MonoBehaviour, IEntityObject
    {
        [SerializeField] private int entity;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D entityCollider;
        [SerializeField] private Transform transformObject;
        
        public Animator Animator => animator;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Transform Transform => transformObject;
        public Collider2D Collider => entityCollider;
        public int Entity => entity;
        
        public void InitializeEntity(int value) => entity = value;
        
        
        
    }
}