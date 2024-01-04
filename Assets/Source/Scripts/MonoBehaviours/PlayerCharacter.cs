using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Source.Scripts.MonoBehaviours
{
    public class PlayerCharacter : Character, IPlayer, ILightable

    {
        [SerializeField] private Light2D light;

        public Light2D Light => light;   
            
        public override void OnDead()
        {
    
        }
    
        public override void OnHit()
        {
    
        }
    
        
        public void SetLightActive(bool value)
        {
            
        }
    }
}