using Source.MonoBehaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui 
{
    public class HealthBarUI : MonoBehaviourUI
    {
        public Slider slider;
        public Gradient gradient;
        public Image fill;

        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;

            fill.color = gradient.Evaluate(1f);
        }

        public void SetHealth(float health)
        {
            slider.value = health;

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
