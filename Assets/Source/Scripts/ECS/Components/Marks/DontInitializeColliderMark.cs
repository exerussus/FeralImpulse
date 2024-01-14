using Source.EasyECS.Interfaces;

namespace Source.Scripts.ECS.Components.Marks
{
    /// <summary>
    /// Коллайдер сущности не проинициализирован и будет обработан системой EntityCollidersSystem.
    /// </summary>

    public struct DontInitializeColliderMark : IEcsMark
    {
        
    }
}