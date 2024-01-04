using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components
{
    public struct HealthData
    {
        
        private float _maxValue;
        private float _currentValue;
        
        public IHealthy Healthy { get; private set; }
        
        public float MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                if (_currentValue > _maxValue) _currentValue = _maxValue;
            }
        }

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