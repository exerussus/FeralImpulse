using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components
{
    public struct TransformData
    {
        public Transform Value;

        public void InitializeValues(IEntityObject entityObject)
        {
            Value = entityObject.Transform;
        }
    }
}