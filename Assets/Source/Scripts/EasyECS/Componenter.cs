using System;
using System.Collections.Generic;
using Source.EasyECS.Interfaces;

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
        
        public ref T AddRequestData<T, TA1, TA2, TA3, TA4>(int entity, TA1 argument1, TA2 argument2, TA3 argument3, TA4 argument4) where T : struct, IEcsRequestData<TA1, TA2, TA3, TA4>
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ref var data = ref ecsPool.Add(entity);
            data.InitializeValues(argument1, argument2, argument3, argument4);
            return ref data;
        }  
        
        public ref T AddRequestData<T, TA1, TA2, TA3>(int entity, TA1 argument1, TA2 argument2, TA3 argument3) where T : struct, IEcsRequestData<TA1, TA2, TA3>
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ref var data = ref ecsPool.Add(entity);
            data.InitializeValues(argument1, argument2, argument3);
            return ref data;
        }  
        
        public ref T AddRequestData<T, TA1, TA2>(int entity, TA1 argument1, TA2 argument2) where T : struct, IEcsRequestData<TA1, TA2>
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ref var data = ref ecsPool.Add(entity);
            data.InitializeValues(argument1, argument2);
            return ref data;
        }   
        
        public ref T AddRequestData<T, TA>(int entity, TA argument) where T : struct, IEcsRequestData<TA>
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ref var data = ref ecsPool.Add(entity);
            data.InitializeValues(argument);
            return ref data;
        }   
        
        public ref T AddData<T, TA1, TA2, TA3, TA4>(int entity, TA1 argument1, TA2 argument2, TA3 argument3, TA4 argument4) where T : struct, IEcsData<TA1, TA2, TA3, TA4>
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ref var data = ref ecsPool.Add(entity);
            data.InitializeValues(argument1, argument2, argument3, argument4);
            return ref data;
        }  
        
        public ref T AddData<T, TA1, TA2, TA3>(int entity, TA1 argument1, TA2 argument2, TA3 argument3) where T : struct, IEcsData<TA1, TA2, TA3>
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ref var data = ref ecsPool.Add(entity);
            data.InitializeValues(argument1, argument2, argument3);
            return ref data;
        }  
        
        public ref T AddData<T, TA1, TA2>(int entity, TA1 argument1, TA2 argument2) where T : struct, IEcsData<TA1, TA2>
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ref var data = ref ecsPool.Add(entity);
            data.InitializeValues(argument1, argument2);
            return ref data;
        }   
        
        public ref T AddData<T, TA>(int entity, TA argument) where T : struct, IEcsData<TA>
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ref var data = ref ecsPool.Add(entity);
            data.InitializeValues(argument);
            return ref data;
        }   
        
        public ref T Add<T>(int entity) where T : struct, IEcsComponent
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            if (!ecsPool.Has(entity)) return ref ecsPool.Add(entity);
            return ref ecsPool.Get(entity);
        }    
        
        public ref T AddOrGet<T>(int entity) where T : struct, IEcsComponent
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            if (!ecsPool.Has(entity)) return ref ecsPool.Add(entity);
            return ref ecsPool.Get(entity);
        }     
        
        public void Del<T>(int entity) where T : struct, IEcsComponent
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            ecsPool.Del(entity);
        }   
        
        public bool Has<T>(int entity) where T : struct, IEcsComponent
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            return ecsPool.Has(entity);
        } 
        
        public ref T Get<T>(int entity) where T : struct, IEcsComponent
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type)) _pools[type] = _world.GetPool<T>();
            var ecsPool = (EcsPool<T>)_pools[type];
            return ref ecsPool.Get(entity);
        }
        
        public ref T GetFirstEntityComponent<T>() where T : struct, IEcsComponent
        {
            foreach (var entity in _world.Filter<T>().End())
            {
                return ref _world.GetPool<T>().Get(entity);
            }
            throw new InvalidOperationException("Фильтр пуст");
        }

    }
}