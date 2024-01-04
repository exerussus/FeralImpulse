using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface IPhysicalBody
    {
        public Rigidbody2D Rigidbody { get; }
        
    }
}