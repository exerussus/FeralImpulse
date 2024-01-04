

using UnityEngine.Rendering;

namespace Source.Scripts.ECS.Components
{
    // Время перед активацией коллайдера оружия
    public struct PreparingWeaponActivatedData
    {
        public float TimeRemaining;

        public void InitializeValues(float value)
        {
            TimeRemaining = value;
        }
        
        
    }
}