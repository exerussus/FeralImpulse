using Source.EasyECS;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.MonoBehaviours;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class AISystem : EasySystem, IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _shouldSleepFilter;
        [EasyInject] private DayNight dayNight;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _shouldSleepFilter = _world.Filter<SleepData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _shouldSleepFilter) SleepCheck(entity);
        }

        private void SleepCheck(int entity)
        {
            float time = dayNight.CurrentTime;
            var sleepData = Componenter.Get<SleepData>(entity);
            bool isSleepTime;
            if (sleepData.SleepTime < sleepData.WakeupTime)
                isSleepTime = time >= sleepData.SleepTime && time < sleepData.WakeupTime;
            else
                isSleepTime = time >= sleepData.SleepTime || time < sleepData.WakeupTime;
            bool hasMark = Componenter.Has<ShouldSleepNowMark>(entity);
            if (isSleepTime && !hasMark)
                Componenter.Add<ShouldSleepNowMark>(entity);
            else if (!isSleepTime && hasMark)
                Componenter.Del<ShouldSleepNowMark>(entity);
        }
    }
}