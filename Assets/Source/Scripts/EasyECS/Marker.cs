using System;
using System.Collections.Generic;

namespace Source.EasyECS
{
    public class Marker: IEcsInitSystem, IEcsSharingSystem
    {
        private EcsWorld _world;

        private Dictionary<Type, IEcsPool> _pools;
        
        public void Init(IEcsSystems systems)
        {
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
        
        public static void Add<T>(EcsPool<T> ecsPool, int entity) where T : struct
        {
            if (!ecsPool.Has(entity)) ecsPool.Add(entity);
        }
    
        public static void Del<T>(EcsPool<T> ecsPool, int entity) where T : struct
        {
            ecsPool.Del(entity);
        }
        
        public static void Add<T>(EcsPoolInject<T> ecsPool, int entity) where T : struct
        {
            if (!ecsPool.Value.Has(entity)) ecsPool.Value.Add(entity);
        }
    
        public static void Del<T>(EcsPoolInject<T> ecsPool, int entity) where T : struct
        {
            ecsPool.Value.Del(entity);
        }
    
        public static bool Has<T>(EcsPool<T> ecsPool, int entity) where T : struct
        {
            return ecsPool.Has(entity);
        }
    
        public static bool Has<T>(EcsPoolInject<T> ecsPool, int entity) where T : struct
        {
            return ecsPool.Value.Has(entity);
        }
    }
}