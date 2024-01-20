using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components.Data
{
    public struct WatcherData: IEcsData<IWatcher>
    {
        public Vector3 FirstPointPosition;
        public Vector3 SecondPointPosition;
        public float PauseTime;
        
        public void InitializeValues(IWatcher watcher)
        {
            FirstPointPosition = watcher.FirstPointPosition;
            SecondPointPosition = watcher.SecondPointPosition;
            PauseTime = watcher.PauseTime;
        }
    }
}