namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Наделяет обьект здоровьем.
    /// </summary>
    
    public interface IHealthy
    {
        public float Health { get; }
        
        /// <summary>Действие при смерти.</summary>
        public void OnDead();
        
        /// <summary>Действие при получении урона.</summary>
        public void OnHit();
    }
}