using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface IDisplayable
    {
        public SpriteRenderer SpriteRenderer { get; }
    }
}