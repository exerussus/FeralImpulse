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
        
        /// <summary>
        /// Позволяет получить фильтр с первым типом включительно, а со вторым в исключении.
        /// </summary>
        
        /// /// <param name="TInc">Включаемый компонент</param>
        /// /// <param name="TExc">Исключаемый компонент</param>
        public EcsFilter GetFilterByTypeWithExcept<TInc, TExc>() 
            where TInc : struct, IEcsComponent 
            where TExc : struct, IEcsComponent
        {
            return _world.Filter<TInc>().Exc<TExc>().End();
        }

        /// <summary>
        /// Позволяет получить фильтр по трём компонентам.
        /// </summary>
        
        /// /// <param name="T1">Включаемый компонент</param>
        /// /// <param name="T2">Включаемый компонент</param>
        /// /// <param name="T3">Включаемый компонент</param>
        public EcsFilter GetFilterByType<T1, T2, T3>() 
            where T1 : struct, IEcsComponent 
            where T2 : struct, IEcsComponent 
            where T3 : struct, IEcsComponent
        {
            return _world.Filter<T1>().Inc<T2>().Inc<T3>().End();
        }
        
        /// <summary>
        /// Позволяет получить фильтр по двум компонентам.
        /// </summary>
        
        /// /// <param name="T1">Включаемый компонент</param>
        /// /// <param name="T2">Включаемый компонент</param>
        public EcsFilter GetFilterByType<T1, T2>() 
            where T1 : struct, IEcsComponent 
            where T2 : struct, IEcsComponent
        {
            return _world.Filter<T1>().Inc<T2>().End();
        }
        
        /// <summary>
        /// Позволяет получить фильтр по одному компоненту.
        /// </summary>
        
        /// /// <param name="T">Включаемый компонент</param>
        public EcsFilter GetFilterByType<T>() where T : struct, IEcsComponent
        {
            return _world.Filter<T>().End();
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
        public void ForeachEntities(EcsFilter ecsFilter, Action<int, bool> action, bool boolValue)
        {
            foreach (var entity in ecsFilter)
            {
                action?.Invoke(entity, boolValue);
            }         
        }

        public void ForeachEntities(EcsFilter ecsFilter, Action<int> action)
        {
            foreach (var entity in ecsFilter)
            {
                action?.Invoke(entity);
            }
        }
        public void ForeachEntities(EcsFilter ecsFilter, Action<int, string, bool> action, string stringValue, bool boolValue)
        {
            foreach (var entity in ecsFilter)
            {
                action?.Invoke(entity, stringValue, boolValue);
            }
        }
    }
}