﻿using Source.EasyECS.Interfaces;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Время перед активацией коллайдера оружия.
    /// </summary>
    
    /// /// <param name="float">TimeRemaining</param>

    public struct PreparingWeaponActivatedData : IEcsData<float>
    {
        /// <summary> Сколько времени осталось. </summary>
        public float TimeRemaining;

        public void InitializeValues(float value)
        {
            TimeRemaining = value;
        }
    }
}