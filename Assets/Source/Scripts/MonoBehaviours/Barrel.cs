using UnityEngine;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.MonoBehaviours
{
    public class Barrel : MonoBehaviour, IEntityObject, IDestructible, IExplosible, IHealthy

    {
        [SerializeField] private int entity;
        [SerializeField] private float radius;
        [SerializeField] private float impulse;
        [SerializeField] private float health;
        [SerializeField] private Collider2D barrelCollider;
        public int Entity => entity;
        public float Radius => radius;
        public float Impulse => impulse;
        public float Health => health;
        public Transform Transform => transform; // а так можно?
        public Collider2D Collider => barrelCollider;
        
        public void InitializeEntity(int value)
        {
            entity = value;
        }
        
        public void OnDead()
        {
           gameObject.SetActive(false);
        }

        public void OnHit()
        {
            
        }
    }
}