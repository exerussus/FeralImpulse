
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components
{
    public struct EntityObjectData
    {
        public IEntityObject Value;

        public void InitializeValues(int entity, IEntityObject entityObject)
        {
            Value = entityObject;
            Value.InitializeEntity(entity);
        }
    }
}