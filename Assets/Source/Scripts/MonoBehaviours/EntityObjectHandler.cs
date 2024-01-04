
using System.Collections.Generic;
using Source.EasyECS;
using Source.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class EntityObjectHandler : EasyMonoBehaviour
    {
        private List<MonoBehaviour> initializeObjects;

        public List<MonoBehaviour> InitializeObjects => initializeObjects;

        
        public override void Initialize()
        {
            TempTestCode.Start();
            initializeObjects = new List<MonoBehaviour>();
            var allObjects = FindObjectsOfType<MonoBehaviour>();
            foreach (var monoBehaviour in allObjects)
            {
                if(monoBehaviour is IEntityObject) initializeObjects.Add(monoBehaviour);
            }
            TempTestCode.End();
        }
    }
}