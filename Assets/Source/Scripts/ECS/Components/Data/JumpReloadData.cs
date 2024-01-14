using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Сколько времени осталось до включения колайдеров после прыжка.
    /// </summary>
    
    /// /// <param name="float">TimeRemaining</param>
    /// /// <param name="Collider2D">GroundChecker</param>
    /// /// <param name="Collider2D">LeftChecker</param>
    /// /// <param name="Collider2D">RightChecker</param>

    public struct JumpReloadData : IEcsData<float, Collider2D, Collider2D, Collider2D>
    {
        /// <summary> Сколько времени осталось. </summary>
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