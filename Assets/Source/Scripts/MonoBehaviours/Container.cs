namespace Source.Scripts.MonoBehaviours
{
    public class Container : Destructible
    {
        public override void OnDead()
        {
            animator.SetBool("IsOpened", true);
        }
        public override void OnHit()
        {
            
        }
    }
}