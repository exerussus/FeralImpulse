using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface ILightable
    {
        public Light2D Light { get; }
        
        public void SetLightActive(bool value);
        
        
    }
}