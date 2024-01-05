using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Отображаемый.
    /// </summary>

    public interface IDisplayable
    {
        public SpriteRenderer SpriteRenderer { get; }
    }
}