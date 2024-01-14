using System.Collections;
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
            spriteRenderer.color = Color.red;
            StartCoroutine(ChangeColorCoroutine());
        }

        private IEnumerator ChangeColorCoroutine()
        {
            yield return new WaitForSeconds(0.3f);
            spriteRenderer.color = Color.white;
        }
    
        
        public void SetLightActive(bool value)
        {
            
        }
    }
}