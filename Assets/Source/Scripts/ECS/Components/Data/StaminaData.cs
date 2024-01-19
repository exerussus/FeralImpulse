using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Holds stamina. Max, current, regen.
    /// </summary>

    /// /// <param name="float">MaxValue</param>
    /// /// <param name="float">CurrentValue</param>
    /// /// <param name="float">Regen</param>
    public struct StaminaData : IEcsData<IStaminaExhaustable>
    {
        public float MaxValue;
        public float CurrentValue;
        public float Regen;

        public void InitializeValues(IStaminaExhaustable argument)
        {
            MaxValue = argument.Stamina;
            CurrentValue = MaxValue;
            Regen = argument.Regen;
        }
    }
}
