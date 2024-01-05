using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components
{
    public struct AnimatorData
    {
        public Animator Value;
        
        
        

        public void InitializeValues(IAnimable animable)
        {
            Value = animable.Animator;
        }
    }
}