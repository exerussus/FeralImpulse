using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Содержит здоровье.
    /// </summary>
    
    /// /// <param name="IHealthy">Healthy</param>
    /// /// <param name="float">MaxValue</param>
    /// /// <param name="float">CurrentValue</param>

    public struct HealthData : IEcsData<IHealthy>
    {
        private float _maxValue;
        private float _currentValue;
        
        public IHealthy Healthy { get; private set; }
        
        /// <summary>Максимальное здоровье.</summary>
        public float MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                if (_currentValue > _maxValue) _currentValue = _maxValue;
            }
        }

        /// <summary>Текущее здоровье.</summary>
        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                var rawValue = value > _maxValue ? _maxValue : value;
                _currentValue = rawValue > 0 ? rawValue : 0;
            }
        }

        public void InitializeValues(IHealthy healthy)
        {
            _maxValue = healthy.Health;
            _currentValue = healthy.Health;
            Healthy = healthy;
        }
    }
}