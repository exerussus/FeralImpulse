using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components
{
    // сила прыжка
    public struct JumpForceData
    {
        public float Value;

        public void InitializeValues(IMovable movable)
        {
            Value = movable.JumpForce;
        }
    }
}