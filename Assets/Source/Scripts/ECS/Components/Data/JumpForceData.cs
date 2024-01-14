using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Cила прыжка.
    /// </summary>
    
    /// /// <param name="float">Value</param>

    public struct JumpForceData : IEcsData<IMovable>
    {
        public float Value;

        public void InitializeValues(IMovable movable)
        {
            Value = movable.JumpForce;
        }
    }
}