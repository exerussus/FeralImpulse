using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components
{
    public struct RigidbodyData
    {
        public Rigidbody2D Value;


        public void InitializeValues(IPhysicalBody physicalBody)
        {
            Value = physicalBody.Rigidbody;
        }
        
        
        
        
    }
}