using Source.Scripts.MonoBehaviours.Abstractions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Source.Scripts.MonoBehaviours
{
    public class Torch : MonoBehaviour, IEntityObject, ILightable, IHealthy
    {
        [SerializeField] private int entity;
        [SerializeField] private Transform torchTransform;
        [SerializeField] private Collider2D torchCollider;
        [SerializeField] private Light2D torchLight;
        [SerializeField] private float health = 1;
        [SerializeField] private GameObject particles;
        
        public Transform Transform => torchTransform;
        public Collider2D Collider => torchCollider;
        public int Entity => entity;
        public Light2D Light => torchLight;
        public float Health => health;
        
        public void InitializeEntity(int value)
        {
            entity = value;
        }

        public void SetLightActive(bool value)
        {
            
        }
        
        public void OnDead()
        {
            torchLight.enabled = false;
            particles.SetActive(false);
        }

        public void OnHit()
        {
            
        }
    }
}