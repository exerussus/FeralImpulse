using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Хранит SpriteRenderer.
    /// </summary>
    
    /// /// <param name="SpriteRenderer">Value</param>

    public struct SpriteRenderData : IEcsData<IDisplayable>
    {
        public SpriteRenderer Value;

        public void InitializeValues(IDisplayable displayable)
        {
            Value = displayable.SpriteRenderer;
        }
    }
}