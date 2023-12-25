using UnityEngine;

namespace Source.MonoBehaviours
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbodyValue;
        
        public Rigidbody2D Rigidbody => rigidbodyValue;
        
    }
}