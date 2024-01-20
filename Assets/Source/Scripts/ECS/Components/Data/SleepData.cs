using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Components.Data
{
    public struct SleepData: IEcsData<ISleepable>
    {
        public Vector3 HomePosition;
        public float SleepTime;
        public float WakeupTime;
        
        public void InitializeValues(ISleepable sleeper)
        {
            HomePosition = sleeper.HomePosition;
            SleepTime = sleeper.SleepTime;
            WakeupTime = sleeper.WakeupTime;
        }
    }
}