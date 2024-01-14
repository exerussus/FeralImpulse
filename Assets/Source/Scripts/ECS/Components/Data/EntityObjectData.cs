using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// Хранит IEntityObject.
    /// </summary>
    
    /// /// <param name="IEntityObject">Value</param>

    public struct EntityObjectData : IEcsData<int, IEntityObject>
    {
        public IEntityObject Value;

        public void InitializeValues(int entity, IEntityObject entityObject)
        {
            Value = entityObject;
            Value.InitializeEntity(entity);
        }
    }
}