using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Скорость персонажа.
    /// </summary>
    
    /// /// <param name="float">Value</param>

    public struct MoveSpeedData : IEcsData<IMovable>
    {
        public float Value;
        
        public void InitializeValues(IMovable movable)
        {
            Value = movable.MoveSpeed;
        }
    }
}