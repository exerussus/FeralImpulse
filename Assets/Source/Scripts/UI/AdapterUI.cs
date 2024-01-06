
using System;
using Source.EasyECS;
using Source.MonoBehaviours;
using Source.Scripts.ECS.Systems;
using Source.Scripts.Ui;

namespace Source.Scripts.UI
{
    public class AdapterUI : EasyMonoBehaviour
    {
        private Binder _binder;
        private InformationSystem _informationSystem;
        
        private HealthBarUI _healthBar;
        
        public override void Initialize()
        {
            _binder = GetSharedMonoBehaviour<Binder>();
            _informationSystem = GetSharedEcsSystem<InformationSystem>();
            
            _healthBar = _binder.GetUIByType<HealthBarUI>();
            
            _informationSystem.OnHealthChange += RefreshHealthBar;


        }

        

        private void RefreshHealthBar()
        {
            _healthBar.SetMaxHealth(_informationSystem.GetPlayerMaxHealth());
            _healthBar.SetHealth(_informationSystem.GetPlayerCurrentHealth());
        }

        private void OnDestroy()
        {
            if (_informationSystem != null) _informationSystem.OnHealthChange -= RefreshHealthBar;
        }
    }
}