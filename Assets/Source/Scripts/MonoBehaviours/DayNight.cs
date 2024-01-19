using System;
using Source.EasyECS;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class DayNight: EasyMonoBehaviour
    {
        [SerializeField] private float timeMultiply = 1000;
        [SerializeField] private float morningTime = 6 * 60 * 60;
        [SerializeField] private float eveningTime = 18 * 60 * 60;
        private float _currentTime = 0;
        private const float SecondsInDay = 24 * 60 * 60;

        private void FixedUpdate()
        {
            _currentTime += timeMultiply * Time.fixedDeltaTime % SecondsInDay;
            //Debug.Log($"{_currentTime} isDay={IsDay()}");
        }

        public bool IsDay()
        {
            return _currentTime > morningTime && _currentTime < eveningTime;
        }
    }
}