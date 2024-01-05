
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components

{
    public struct MoveSpeedData
    {
        public float Value;
        
        public void InitializeValues(IMovable movable)
        {
            Value = movable.MoveSpeed;
        }
    }
}