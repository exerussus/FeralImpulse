using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Source.EasyECS
{
    public abstract class EasySystem 
    {
        private bool _isInitialized = false;
        private GameShare _gameShare;
        protected Componenter Componenter;
        
        public void PreInit(GameShare gameShare)
        {
            if (_isInitialized) return;
            _gameShare = gameShare;
            Componenter = GetSharedEcsSystem<Componenter>();
            InjectFields();
            _isInitialized = true;
        }

        public T GetSharedMonoBehaviour<T>() where T : EasyMonoBehaviour
        {
            return _gameShare.GetSharedMonoBehaviour<T>();
        }
        
        public T GetSharedEcsSystem<T>() where T : IEcsSharingSystem
        {
            return _gameShare.GetSharedEcsSystem<T>();
        }

        public void InjectFields()
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            foreach (var field in fields)
            {
                var injectAttribute = (EasyInjectAttribute)field.GetCustomAttributes(typeof(EasyInjectAttribute), true).FirstOrDefault();

                if (injectAttribute != null)
                {
                    var fieldType = field.FieldType;

                    if (typeof(EasySystem).IsAssignableFrom(fieldType) && typeof(IEcsSharingSystem).IsAssignableFrom(fieldType))
                    {
                        var sharedEasySystemMethod = typeof(EasySystem).GetMethod("GetSharedEcsSystem").MakeGenericMethod(fieldType);
                        var sharedSystem = sharedEasySystemMethod.Invoke(this, null);

                        field.SetValue(this, sharedSystem);
                    }
                    else if (typeof(EasyMonoBehaviour).IsAssignableFrom(fieldType))
                    {
                        var sharedMonoBehaviourMethod = typeof(EasySystem).GetMethod("GetSharedMonoBehaviour").MakeGenericMethod(fieldType);
                        var sharedMonoBehaviour = sharedMonoBehaviourMethod.Invoke(this, null);

                        field.SetValue(this, sharedMonoBehaviour);
                    }
                }
            }
        }
    }
}