using System;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class CameraFollow : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _cameraOffset;

        private void Start()
        {
            _camera = Camera.main;
            _cameraOffset = _camera.transform.position - transform.position;

        }

        private void LateUpdate()
        {
            _camera.transform.position = transform.position + _cameraOffset;


        }

    }
}