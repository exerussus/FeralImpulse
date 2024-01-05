using Source.EasyECS.Interfaces;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Отмашка после удара.
    /// </summary>
    
    /// /// <param name="float">TimeRemaining</param>
    
    public struct AfterKickWeaponData : IEcsData<float>
    {
        /// <summary> Сколько времени осталось. </summary>
        public float TimeRemaining;

        public void InitializeValues(float value)
        {
            TimeRemaining = value;
        }
        
    }
}