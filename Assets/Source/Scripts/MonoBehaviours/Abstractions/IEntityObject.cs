using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface IEntityObject
    {
        public Collider2D Collider { get; }
        public int Entity { get; }
        public void InitializeEntity(int value);
    }
}