using System.Collections;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class Barrier : Destructible
    {
        public override void OnDead()
        {
            Collider.enabled = false;
            spriteRenderer.enabled = false;
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

    }
}