
using System.Linq;
using System.Reflection;
using Source.MonoBehaviours;
using UnityEngine;
using Binder = Source.MonoBehaviours.Binder;

namespace Source.EasyECS
{
    public abstract class EasyMonoBehaviour : MonoBehaviour
    {
        private DataPack _dataPack;
        private bool _isInitialized = false;
        private GameShare _gameShare;
        
        public void PreInit(GameShare gameShare, DataPack dataPack)
        {
            if (_isInitialized) return;
            _gameShare = gameShare;
            _dataPack = dataPack;
            _isInitialized = true;
        }

        public virtual void Initialize(){}

        public T GetSharedMonoBehaviour<T>() where T : EasyMonoBehaviour
        {
            return _gameShare.GetSharedMonoBehaviour<T>();
        }
        
        public T GetSharedEcsSystem<T>() where T : IEcsSharingSystem
        {
            return _gameShare.GetSharedEcsSystem<T>();
        }

        public void Inject()
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            foreach (var field in fields)
            {
                var injectAttribute = (EasyInjectAttribute)field.GetCustomAttributes(typeof(EasyInjectAttribute), true).FirstOrDefault();

                if (injectAttribute != null)
                {
                    var fieldType = field.FieldType;

                    if (typeof(EasyMonoBehaviour).IsAssignableFrom(fieldType))
                    {
                        var sharedMonoBehaviourMethod = typeof(EasyMonoBehaviour).GetMethod("GetSharedMonoBehaviour").MakeGenericMethod(fieldType);
                        var sharedMonoBehaviour = sharedMonoBehaviourMethod.Invoke(this, null);

                        field.SetValue(this, sharedMonoBehaviour);
                    }
                    else if (typeof(EasySystem).IsAssignableFrom(fieldType) && typeof(IEcsSharingSystem).IsAssignableFrom(fieldType))
                    {
                        var sharedEasySystemMethod = typeof(EasyMonoBehaviour).GetMethod("GetSharedEcsSystem").MakeGenericMethod(fieldType);
                        var sharedSystem = sharedEasySystemMethod.Invoke(this, null);

                        field.SetValue(this, sharedSystem);
                    }
                    else if (typeof(MonoBehaviourUI).IsAssignableFrom(fieldType))
                    {
                        var binder = GetSharedMonoBehaviour<Binder>();

                        var sharedEasySystemMethod = typeof(Binder).GetMethod("GetUIByType").MakeGenericMethod(fieldType);
                        var sharedSystem = sharedEasySystemMethod.Invoke(binder, null);

                        field.SetValue(this, sharedSystem);
                    }
                }
            }
        }
    }
}