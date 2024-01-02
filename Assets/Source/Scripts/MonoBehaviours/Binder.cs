using System;
using System.Collections.Generic;
using Source.EasyECS;
using UnityEngine;

namespace Source.MonoBehaviours
{
    public class Binder : EasyMonoBehaviour
    {
        [SerializeField] private List<MonoBehaviourUI> elementsUI;
        private Dictionary<Type, MonoBehaviourUI> _dataUI;

        public override void Initialize()
        {
            _dataUI = new Dictionary<Type, MonoBehaviourUI>();
            foreach (var elementUI in elementsUI)
            {
                _dataUI[elementUI.GetType()] = elementUI;
                elementUI.Init();
            }
        }

        public T GetUIByType<T>() where T : MonoBehaviourUI
        {
            return (T)_dataUI[typeof(T)];
        }
    }
}