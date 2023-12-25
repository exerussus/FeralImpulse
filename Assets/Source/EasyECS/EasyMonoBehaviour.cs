
using UnityEngine;

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

        protected T GetSharedMonoBehaviour<T>() where T : EasyMonoBehaviour
        {
            return _gameShare.GetSharedData<T>();
        }
        
        protected T GetSharedEcsSystem<T>() where T : IEcsSharingSystem
        {
            return _gameShare.GetSharedEcsSystem<T>();
        }
    }
}