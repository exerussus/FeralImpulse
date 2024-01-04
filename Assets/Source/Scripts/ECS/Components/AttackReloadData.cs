namespace Source.Scripts.ECS.Components
{
    // содержит оставшееся время до перезарядки 
    public struct AttackReloadData
    {
        public float TimeRemaining;

        public void InitializeValues(float value)
        {
            TimeRemaining = value;
        }
    }
}