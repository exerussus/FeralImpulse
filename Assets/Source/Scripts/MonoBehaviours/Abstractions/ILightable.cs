
using UnityEngine.Rendering.Universal;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Является источником света.
    /// </summary>

    public interface ILightable
    {
        public Light2D Light { get; }
        
        public void SetLightActive(bool value);
    }
}