using System;

namespace Source.EasyECS
{
    public class Filter : IEcsInitSystem, IEcsSharingSystem
    {
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        public bool AnyExist(EcsFilter filter)
        {
            foreach (var _ in filter)
            {
                return true;
            }
            return false;
        }

        public int Length(EcsFilter filter)
        {
            var length = 0;
            
            foreach (var _ in filter)
            {
                length++;
            }
            return length;
        }
        
        public int GetFirstEntity(EcsFilter filter)
        {
            foreach (var entity in filter)
            {
                return entity;
            }
            throw new InvalidOperationException("Фильтр пуст");
        }
    }
}