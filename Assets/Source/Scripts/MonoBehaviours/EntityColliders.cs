using System.Collections.Generic;
using Source.EasyECS;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class EntityColliders: EasyMonoBehaviour
    {
        private Dictionary<Collider2D, IEntityObject> _entityObjects;
        private Dictionary<IEntityObject, Collider2D> _entityObjectsRevers;

        public override void Initialize()
        {
            _entityObjects = new Dictionary<Collider2D, IEntityObject>();
            _entityObjectsRevers = new Dictionary<IEntityObject, Collider2D>();
        }

        public bool HasObject(Collider2D objectsCollider)
        {
            return _entityObjects.ContainsKey(objectsCollider);
        }

        public void AddObject(Collider2D objectsCollider, IEntityObject entityObject)
        {
            _entityObjects[objectsCollider] = entityObject;
            _entityObjectsRevers[entityObject] = objectsCollider;
        }

        public IEntityObject GetObject(Collider2D objectsCollider)
        {
            return _entityObjects[objectsCollider];
        }

        public void DeleteObject(IEntityObject entityObject)
        {
            var objectCollider = _entityObjectsRevers[entityObject];
            _entityObjectsRevers.Remove(entityObject);
            _entityObjects.Remove(objectCollider);
        }

        public void DeleteObject(Collider2D objectCollider)
        {
            if (!_entityObjects.ContainsKey(objectCollider)) return;
            var entityObject = _entityObjects[objectCollider];
            _entityObjectsRevers.Remove(entityObject);
            _entityObjects.Remove(objectCollider);
        }
    }
}