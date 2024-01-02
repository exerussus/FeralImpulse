using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class GroundChecker : MonoBehaviour
    {
        public bool IsOnGround { get; private set; }

        private void OnTriggerStay2D(Collider2D other)
        {
            IsOnGround = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            IsOnGround = false;
        }
    }
}