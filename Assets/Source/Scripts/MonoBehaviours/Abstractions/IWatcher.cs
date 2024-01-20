using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface IWatcher
    {
        public Vector3 FirstPointPosition { get; }
        public Vector3 SecondPointPosition { get; }
        public float PauseTime { get; }
    }
}