using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class SideChecker : MonoBehaviour
    {
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