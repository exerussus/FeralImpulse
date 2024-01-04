using System;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class WeaponColliderHandler : MonoBehaviour
    {
        [SerializeField] private WeaponCollider upWeapon;
        [SerializeField] private WeaponCollider middleWeapon;
        [SerializeField] private WeaponCollider downWeapon;

        public WeaponCollider CurrentWeapon { get; private set; }

        public void Prepare(WeaponColliderType weaponType)
        {
            switch (weaponType)
            {
                case WeaponColliderType.Up:
                    CurrentWeapon = upWeapon;
                    break;
                case WeaponColliderType.Middle:
                    CurrentWeapon = middleWeapon;
                    break;
                case WeaponColliderType.Down:
                    CurrentWeapon = downWeapon;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null);
            }
        }
        
        public void Activate()
        {
            CurrentWeapon.Activate();
        }

        public void Deactivate()
        {
            CurrentWeapon.Deactivate();
        }

    }

    public enum WeaponColliderType
    {
        Up,
        Middle,
        Down
    }
    
}