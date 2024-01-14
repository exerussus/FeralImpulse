using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Abstractions
{
    /// <summary>
    /// Является движиемым телом.
    /// </summary>

    public interface IPhysicalBody
    {
        public Rigidbody2D Rigidbody { get; }
    }
}