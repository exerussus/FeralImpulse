using Source.Scripts.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components
{
    public struct WeaponHandlerData
    {
        public WeaponHandler Value;

        public void InitializeValues(IWeaponable weaponable)
        {
            Value = weaponable.WeaponHandler;
        }
    }
}