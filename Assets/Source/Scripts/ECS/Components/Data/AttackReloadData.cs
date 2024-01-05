using Source.EasyECS.Interfaces;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Cодержит оставшееся время до перезарядки.
    /// </summary>
    
    /// /// <param name="float">TimeRemaining</param>

    public struct AttackReloadData : IEcsData<float>
    {
        /// <summary> Сколько времени осталось. </summary>
        public float TimeRemaining;

        public void InitializeValues(float value)
        {
            TimeRemaining = value;
        }
    }
}