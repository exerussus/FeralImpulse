using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components.Data
{
    public struct AnimatorData : IEcsData<IAnimable>
    {
        public Animator Value;
        
        public void InitializeValues(IAnimable animable)
        {
            Value = animable.Animator;
        }
    }
}