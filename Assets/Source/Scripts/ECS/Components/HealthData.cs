namespace Source.Scripts.ECS.Components
{
    public struct HealthData
    {
        private float _maxValue;
        private float _currentValue;
        
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
            set => _currentValue = value > _maxValue ? _maxValue : value;
        }

        public void InitializeValues(float value)
        {
            _maxValue = value;
            _currentValue = value;
        }
    }
}