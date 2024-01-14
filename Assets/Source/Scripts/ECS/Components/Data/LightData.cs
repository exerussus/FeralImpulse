using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine.Rendering.Universal;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Хранит ILightable и Light2D.
    /// </summary>
    
    /// /// <param name="ILightable">Lightable</param>
    /// /// <param name="Light2D">Light</param>

    public struct LightData : IEcsData<ILightable>
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