using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class SideChecker : MonoBehaviour
    {
        [SerializeField] private Collider2D sideCollider;

        public Collider2D Collider => sideCollider;
        public bool IsTouched { get; private set; }

        private void OnTriggerStay2D(Collider2D other)
        {
            IsTouched = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            IsTouched = false;
        }
    }
}