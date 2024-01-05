using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components
{
    public struct SpriteRenderData
    {
        public SpriteRenderer Value;

        public void InitializeValues(IDisplayable displayable)
        {
            Value = displayable.SpriteRenderer;
        }
    }
}