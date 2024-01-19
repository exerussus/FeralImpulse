namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Can blow up - to kick IMovable objects around it from itself
    /// </summary>
    
    public interface IExplosible
    {
        public float Radius { get; }
        public float Impulse { get; }
    }
}