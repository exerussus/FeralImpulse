using System;
using System.Collections.Generic;

namespace Source.EasyECS
{
    public class Componenter : IEcsInitSystem, IEcsSharingSystem
    {
        private EcsWorld _world;

        private Dictionary<Type, IEcsPool> _pools;
        
        public void Init(IEcsSystems systems)
        {
            _pools = new Dictionary<Type, IEcsPool>();
            _world = systems.GetWorld();
        }
        
        public ref T Add<T>(int entity) where T : struct
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            if (!ecsPool.Has(entity)) return ref ecsPool.Add(entity);
            return ref ecsPool.Get(entity);
        }        
        
        public ref T AddOrGet<T>(int entity) where T : struct
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            if (!ecsPool.Has(entity)) return ref ecsPool.Add(entity);
            return ref ecsPool.Get(entity);
        }     
        
        public void Del<T>(int entity) where T : struct
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ecsPool.Del(entity);
        }   
        
        public bool Has<T>(int entity) where T : struct
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            return ecsPool.Has(entity);
        } 
        
        public ref T Get<T>(int entity) where T : struct
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            return ref ecsPool.Get(entity);
        }
        
        public ref T GetFirstEntityComponent<T>() where T : struct
        {
            foreach (var entity in _world.Filter<T>().End())
            {
                return ref _world.GetPool<T>().Get(entity);
            }
            throw new InvalidOperationException("Фильтр пуст");
        }

    }
}