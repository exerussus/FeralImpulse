using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Хранит Transform.
    /// </summary>
    
    /// /// <param name="Transform">Value</param>

    public struct TransformData : IEcsData<IEntityObject>
    {
        public Transform Value;

        public void InitializeValues(IEntityObject entityObject)
        {
            Value = entityObject.Transform;
        }
    }
}