using Source.EasyECS.Interfaces;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Оружее активно и ловит коллайдер противника.
    /// </summary>
    
    /// /// <param name="float">TimeRemaining</param>

    public struct WeaponActivatedData : IEcsData<float>
    {
        /// <summary> Сколько времени осталось. </summary>
        public float TimeRemaining;

        public void InitializeValues(float value)
        {
            TimeRemaining = value;
        }
    }
}