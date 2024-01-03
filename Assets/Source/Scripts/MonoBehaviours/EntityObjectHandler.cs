
using Source.EasyECS;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class EntityObjectHandler : EasyMonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] initializeObjects;

        public MonoBehaviour[] InitializeObjects => initializeObjects;
    }
}