
using System;
using Source.EasyECS;
using Source.MonoBehaviours;
using Source.Scripts.ECS.Systems;
using Source.Scripts.UI.ElementsUI;

namespace Source.Scripts.UI
{
    public class AdapterUI : EasyMonoBehaviour
    {
        private Binder _binder;
        private InformationSystem _informationSystem;
        
        private HealthBarUI _healthBar;
        private StaminaBarUI _staminaBar;
        
        public override void Initialize()
        {
            _binder = GetSharedMonoBehaviour<Binder>();
            _informationSystem = GetSharedEcsSystem<InformationSystem>();
            
            _healthBar = _binder.GetUIByType<HealthBarUI>();
            _staminaBar = _binder.GetUIByType<StaminaBarUI>();
            
            _informationSystem.OnHealthChange += RefreshHealthBar;
            _informationSystem.OnStaminaChange += RefreshStaminaBar;
        } 
        
        private void RefreshHealthBar()
        {
            _healthBar.SetMaxHealth(_informationSystem.GetPlayerMaxHealth());
            _healthBar.SetHealth(_informationSystem.GetPlayerCurrentHealth());
        }

        private void RefreshStaminaBar()
        {
            _staminaBar.SetValue(_informationSystem.GetMaxStamina(), _informationSystem.GetCurrentStamina());
        }

        private void OnDestroy()
        {
            if (_informationSystem != null) _informationSystem.OnHealthChange -= RefreshHealthBar;
            if (_informationSystem != null) _informationSystem.OnStaminaChange -= RefreshStaminaBar;
        }
    }
}