using Source.Scripts.MonoBehaviours;
using UnityEngine;

namespace Source.Scripts.ECS.Components
{
    public struct JumpReloadData
    {
        public float TimeRemaining;
        public Collider2D GroundChecker;
        public Collider2D LeftChecker;
        public Collider2D RightChecker;

        public void InitializeValues(float value, Collider2D groundChecker, Collider2D leftChecker, Collider2D rightChecker)
        {
            TimeRemaining = value;
            GroundChecker = groundChecker;
            LeftChecker = leftChecker;
            RightChecker = rightChecker;
        }
    }
}