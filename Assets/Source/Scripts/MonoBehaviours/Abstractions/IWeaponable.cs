namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Имеет оружие.
    /// </summary>

    public interface IWeaponable
    {
        public WeaponHandler WeaponHandler { get; }
    }
}