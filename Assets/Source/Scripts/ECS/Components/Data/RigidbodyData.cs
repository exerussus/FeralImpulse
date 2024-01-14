using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Хранит Rigidbody2D.
    /// </summary>
    
    /// /// <param name="Rigidbody2D">Value</param>

    public struct RigidbodyData : IEcsData<IPhysicalBody>
    {
        public Rigidbody2D Value;
        
        public void InitializeValues(IPhysicalBody physicalBody)
        {
            Value = physicalBody.Rigidbody;
        }
    }
}