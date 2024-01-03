using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class WeaponCollider : MonoBehaviour
    {
        [SerializeField] private Collider2D weaponArea;
        [SerializeField] private Collider2D originCharacter;
        private List<Collider2D> _detected = new List<Collider2D>();
        private List<Collider2D> _ignore = new List<Collider2D>();
        
        
        public List<Collider2D> Detected => _detected;

        public void Activate()
        {
            weaponArea.enabled = true;
        }

        public void Deactivate()
        {
            weaponArea.enabled = false;
            _ignore = new List<Collider2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (originCharacter == other) return;
            if (_ignore.Contains(other)) return;
            _detected.Add(other);
            _ignore.Add(other);
        }
        
        
    }
}