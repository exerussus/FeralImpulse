using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    public interface IWatcher
    {
        public Vector3 PatrolStartPosition { get; }
        public Vector3 PatrolEndPosition { get; }
        public float PauseTime { get; }
    }
}