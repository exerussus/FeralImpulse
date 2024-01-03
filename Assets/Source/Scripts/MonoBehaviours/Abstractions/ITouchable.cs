using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface ITouchable
    {
        public Collider2D Collider { get; }
    }
}