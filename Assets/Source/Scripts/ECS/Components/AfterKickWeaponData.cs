namespace Source.Scripts.ECS.Components
{
    // Отмашка после удара
    public struct AfterKickWeaponData
    {
        public float TimeRemaining;

        public void InitializeValues(float value)
        {
            TimeRemaining = value;
        }
        
    }
}