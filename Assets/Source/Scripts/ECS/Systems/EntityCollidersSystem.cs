﻿using System.Collections.Generic;
using Source.EasyECS;
using Source.Scripts.ECS.Components;
using Source.Scripts.ECS.Components.Data;
using Source.Scripts.ECS.Components.Marks;
using Source.Scripts.ECS.Components.Requests;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class EntityCollidersSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Componenter _componenter;
        private EcsFilter _initializeFilter;
        private EcsFilter _activatedWeaponFilter;
        
        private Dictionary<Collider2D, IEntityObject> _entityObjects;
        private Dictionary<IEntityObject, Collider2D> _entityObjectsRevers;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _componenter = systems.GetSharedEcsSystem<Componenter>();
            _initializeFilter = _world.Filter<EntityObjectData>().Inc<DontInitializeColliderMark>().End();
            _activatedWeaponFilter = _world.Filter<WeaponHandlerData>().Inc<WeaponActivatedData>().End();
            _entityObjects = new Dictionary<Collider2D, IEntityObject>();
            _entityObjectsRevers = new Dictionary<IEntityObject, Collider2D>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _initializeFilter) InitializeCollider(entity);
            foreach (var entity in _activatedWeaponFilter) DetectWeaponCollisions(entity);

        }

        private void InitializeCollider(int entity)
        {
            _componenter.Del<DontInitializeColliderMark>(entity);
            ref var entityObjectData = ref _componenter.Get<EntityObjectData>(entity);
            _entityObjects[entityObjectData.Value.Collider] = entityObjectData.Value;
            _entityObjectsRevers[entityObjectData.Value] = entityObjectData.Value.Collider;
        }

        private void DetectWeaponCollisions(int originEntity)
        {
            ref var weaponColliderData = ref _componenter.Get<WeaponHandlerData>(originEntity);
            var detectedList = weaponColliderData.Value.CurrentWeapon.Detected;

            if (detectedList.Count == 0) return;

            for (int i = detectedList.Count - 1; i >= 0; i--)
            {
                var detectedCollider = detectedList[i];
                detectedList.Remove(detectedCollider);
                if (!_entityObjects.ContainsKey(detectedCollider)) continue;
                
                var targetEntity = _entityObjects[detectedCollider].Entity;

                if (!_componenter.Has<HealthData>(targetEntity)) continue;
                if (_componenter.Has<HealthWeaponDamageRequestData>(originEntity))
                {
                    ref var healthWeaponDamageRequest = ref _componenter.Get<HealthWeaponDamageRequestData>(originEntity);
                    healthWeaponDamageRequest.TargetEntities.Add(targetEntity);
                }
                else
                {
                    ref var healthWeaponDamageRequest = ref _componenter.Add<HealthWeaponDamageRequestData>(originEntity);
                    healthWeaponDamageRequest.InitializeValues(originEntity, new List<int>());
                    healthWeaponDamageRequest.TargetEntities.Add(targetEntity);
                }
            }
        }
    }
}