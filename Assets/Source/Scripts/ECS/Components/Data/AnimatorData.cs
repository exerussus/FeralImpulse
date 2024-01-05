using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Хранит Animator.
    /// </summary>
    
    /// /// <param name="Animator">Value</param>

    public struct AnimatorData : IEcsData<IAnimable>
    {
        public Animator Value;
        
        public void InitializeValues(IAnimable animable)
        {
            Value = animable.Animator;
        }
    }
}