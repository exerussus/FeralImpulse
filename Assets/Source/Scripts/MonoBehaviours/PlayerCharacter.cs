using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Source.Scripts.MonoBehaviours
{
    public class PlayerCharacter : Character, IPlayer, ILightable

    {
        [SerializeField] private Light2D characterLight;

        public Light2D Light => characterLight;   
            
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