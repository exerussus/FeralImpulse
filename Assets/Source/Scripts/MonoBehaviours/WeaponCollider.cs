using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class WeaponCollider : MonoBehaviour
    {
        public List<Collider2D> Detected { get; }
    }
}