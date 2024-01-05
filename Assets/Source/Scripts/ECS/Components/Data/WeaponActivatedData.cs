
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components
{
    // оружее активно и ловит коллайдер противника
    public struct WeaponActivatedData
    {
        public float TimeRemaining;

        public void InitializeValues(float value)
        {
            TimeRemaining = value;
        }
    }
}