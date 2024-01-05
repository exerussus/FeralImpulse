using System;
using System.Collections.Generic;
using System.Linq;
using Source.EasyECS.Interfaces;

namespace Source.EasyECS
{
    public class Filter : IEcsInitSystem, IEcsSharingSystem
    {
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public int[] GetEntitiesByAnyType<T1, T2, T3>() 
            where T1 : struct, IEcsComponent 
            where T2 : struct, IEcsComponent
            where T3 : struct, IEcsComponent
        {
            var filter1 = _world.Filter<T1>().End();
            var filter2 = _world.Filter<T2>().End();
            var filter3 = _world.Filter<T3>().End();

            var entitySet = new HashSet<int>();

            foreach (var entity in filter1) entitySet.Add(entity);
            foreach (var entity in filter2) entitySet.Add(entity);
            foreach (var entity in filter3) entitySet.Add(entity);

            return entitySet.ToArray();
        }

        public int[] GetEntitiesByAnyType<T1, T2>() 
            where T1 : struct, IEcsComponent 
            where T2 : struct, IEcsComponent
        {
            var filter1 = _world.Filter<T1>().End();
            var filter2 = _world.Filter<T2>().End();
            
            var entitySet = new HashSet<int>();
            
            foreach (var entity in filter1) entitySet.Add(entity);
            foreach (var entity in filter2) entitySet.Add(entity);

            return entitySet.ToArray();
        }

        public int[] GetEntitiesByAnyFilter(EcsFilter[] filters) 
        {
            var entitySet = new HashSet<int>();
            foreach (var ecsFilter in filters) foreach (var entity in ecsFilter) entitySet.Add(entity);
            return entitySet.ToArray();
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