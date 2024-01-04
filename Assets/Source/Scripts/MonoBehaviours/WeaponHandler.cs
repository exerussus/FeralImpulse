using System;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Weapon upWeapon;
        [SerializeField] private Weapon middleWeapon;
        [SerializeField] private Weapon downWeapon;

        public Weapon CurrentWeapon { get; private set; }

        public void Prepare(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Up:
                    CurrentWeapon = upWeapon;
                    break;
                case WeaponType.Middle:
                    CurrentWeapon = middleWeapon;
                    break;
                case WeaponType.Down:
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

    public enum WeaponType
    {
        Up,
        Middle,
        Down
    }
    
}