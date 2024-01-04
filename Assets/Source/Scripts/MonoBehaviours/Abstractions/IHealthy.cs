namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface IHealthy
    {
        public float Health { get; }
        
        public void OnDead();
        public void OnHit();
    }
}