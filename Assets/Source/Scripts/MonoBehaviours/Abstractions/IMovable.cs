namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Может передвигаться.
    /// </summary>

    public interface IMovable
    {
        public float MoveSpeed { get; }
        public float JumpForce { get; }
    }
}