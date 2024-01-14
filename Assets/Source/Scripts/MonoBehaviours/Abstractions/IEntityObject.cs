using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Является сущностью в ECS.
    /// </summary>

    public interface IEntityObject
    {
        public Transform Transform { get; }
        public Collider2D Collider { get; }
        public int Entity { get; }
        public void InitializeEntity(int value);
        
    }
}