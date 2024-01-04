using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components
{
    public struct TransformData
    {
        public Transform Value;

        public void InitializeValues(IPhysicalBody physicalBody)
        {
            Value = physicalBody.Transform;
        }
    }
}