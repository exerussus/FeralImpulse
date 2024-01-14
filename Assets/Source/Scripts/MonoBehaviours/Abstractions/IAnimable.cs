using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Является анимированым.
    /// </summary>

    public interface IAnimable
    {
        public Animator Animator { get; }
    }
}