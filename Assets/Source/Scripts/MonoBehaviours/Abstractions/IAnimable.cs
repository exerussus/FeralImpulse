using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface IAnimable
    {
        public Animator Animator { get; }
    }
}