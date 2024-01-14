using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Хранит WeaponHandler.
    /// </summary>
    
    /// /// <param name="WeaponHandler">Value</param>

    public struct WeaponHandlerData : IEcsData<IWeaponable>
    {
        public WeaponHandler Value;

        public void InitializeValues(IWeaponable weaponable)
        {
            Value = weaponable.WeaponHandler;
        }
    }
}