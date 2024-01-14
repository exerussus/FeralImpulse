
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class AnimatorEffect: MonoBehaviour
    {
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private Transform sideLineFront;
        [SerializeField] private Transform sideLineBack;

        private GameObject _gameObject;
        private float _delay = 0.3f;
        private float _timer;

        private void Start()
        {
            _gameObject = Instantiate(effectPrefab);
            _gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            if(_timer > _delay) _gameObject.SetActive(false);
        }

        private void ShowFront()
        {
            _timer = 0;
            _gameObject.transform.position = sideLineFront.position;
            _gameObject.SetActive(true);
        }
        private void ShowBack()
        {
            _timer = 0;
            _gameObject.transform.position = sideLineBack.position;
            _gameObject.SetActive(true);
        }
        
    }
}