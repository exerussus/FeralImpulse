namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Has stamina.
    /// </summary>

    public interface IStaminaExhaustable
    {
        public float Stamina { get; }
        public float Regen { get; }
    }
}
