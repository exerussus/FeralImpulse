using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine.Rendering.Universal;

namespace Source.Scripts.ECS.Components
{
    public struct LightData
    {
        public ILightable Lightable;
        public Light2D Light;
        
        
        public void InitializeValues(ILightable lightable)
        {
            Lightable = lightable;
            Light = lightable.Light;
        }
    }
}