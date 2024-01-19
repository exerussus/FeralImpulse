using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface ISleepable
    {
        public Vector3 HomePosition { get; }
        public float SleepTime { get; }
        public float WakeupTime { get; }
    }
}