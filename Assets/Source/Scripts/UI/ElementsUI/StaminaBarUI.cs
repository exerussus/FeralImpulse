using UnityEngine;
using UnityEngine.UI;
using Source.MonoBehaviours;

namespace Source.Scripts.UI.ElementsUI
{
    public class StaminaBarUI : MonoBehaviourUI
    {
        public Slider slider;

        public void SetValue(float maxValue, float value)
        {
            slider.maxValue = maxValue;
            slider.value = value;
        }
    }
}
