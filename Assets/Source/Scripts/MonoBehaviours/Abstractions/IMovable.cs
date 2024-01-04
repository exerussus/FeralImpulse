namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface IMovable
    {
        public float MoveSpeed { get; }
        public float JumpForce { get; }
    }
}