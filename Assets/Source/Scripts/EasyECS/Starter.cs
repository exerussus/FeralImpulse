
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.EasyECS
{
    public abstract class Starter : EasyMonoBehaviour
    {
        [SerializeField] private List<SystemState> _updateSystemState;
        [SerializeField] private List<SystemState> _fixedUpdateSystemState;
        [SerializeField] private List<SystemState> _lateUpdateSystemState;
        [SerializeField] private List<string> _bootQueue;
        
        private GameShare _gameSharing;
        private EcsWorld _world;
        private IEcsSystems _stepByStepSystems;
        private IEcsSystems _cardViewRefreshSystems;
        private IEcsSystems _coreSystems;
        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        private IEcsSystems _lateUpdateSystems;

        public void SetSharedData(GameShare gameShare)
        {
            _gameSharing = gameShare;
        }

        public void PreInit()
        {
            _world = new EcsWorld();
            
            PrepareCoreSystems();
            PrepareInitSystems();
            PrepareUpdateSystems();
            PrepareFixedUpdateSystems();
            PrepareLateUpdateSystems();
            DependencyInject();
        }

        private void DependencyInject()
        {
            InjectSystems(_coreSystems);
            InjectSystems(_initSystems);
            InjectSystems(_updateSystems);
            InjectSystems(_fixedUpdateSystems);
            InjectSystems(_lateUpdateSystems);
        }

        private void InjectSystems(IEcsSystems systems)
        {
            foreach (var system in systems.GetAllSystems())
            {
                if (system is EasySystem easySystem)
                {
                    easySystem.PreInit(_gameSharing);
                }
            }
        }
        
        public override void Initialize()
        {
            InitBootInfo();
            
            _coreSystems.Init();
            _initSystems.Init();
            _updateSystems.Init();
            _fixedUpdateSystems.Init();
            _lateUpdateSystems.Init();
        }
        
        protected abstract void SetInitSystems(IEcsSystems initSystems);
        protected abstract void SetUpdateSystems(IEcsSystems updateSystems);
        protected abstract void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems);
        protected abstract void SetLateUpdateSystems(IEcsSystems lateUpdateSystems);

        private void InitBootInfo()
        {
            _bootQueue = new List<string>();
            AddToBoot(_coreSystems);
            AddToBoot(_initSystems);
            AddToBoot(_updateSystems);
            AddToBoot(_fixedUpdateSystems);
            AddToBoot(_lateUpdateSystems);
        }

        private void AddToBoot(IEcsSystems systems)
        {
            foreach (var system in systems.GetAllSystems())
            {
                _bootQueue.Add(system.GetType().Name);
            }
        }
        
        private void AddToShare(IEcsSystems systems)
        {
            var sharingSystems = systems.GetAllSharingSystems();
            if (sharingSystems.Count < 1) return;
            foreach (var sharingSystem in sharingSystems)
            {
                var type = sharingSystem.GetType();
                _gameSharing.AddSharedEcsSystem(type, sharingSystem);
            }
        }
        
        protected void Update() 
        {
            _updateSystems?.Run();
        }

        protected void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }
        protected void LateUpdate()
        {
            _lateUpdateSystems?.Run();
        }

        private void PrepareCoreSystems()
        {
            _coreSystems = new EcsSystems(_world, _gameSharing);
            _coreSystems.Add(new Componenter());
            _coreSystems.Add(new Filter());
            _coreSystems.Add(new Marker());
            _coreSystems.Inject();
            AddToShare(_coreSystems);
        }
        
        private void PrepareInitSystems()
        {
            _initSystems = new EcsSystems(_world, _gameSharing);
            SetInitSystems(_initSystems);
            _initSystems.Inject();
            AddToShare(_initSystems);
        }
        
        private void PrepareUpdateSystems()
        {
            _updateSystems = new EcsSystems(_world, _gameSharing);
            SetUpdateSystems(_updateSystems);
            _updateSystems.Inject();
            _updateSystemState = _updateSystems.SystemsState;
            AddToShare(_updateSystems);
        }
        
        private void PrepareFixedUpdateSystems()
        {
            _fixedUpdateSystems = new EcsSystems(_world, _gameSharing);
            SetFixedUpdateSystems(_fixedUpdateSystems);
            
#if UNITY_EDITOR
                // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                _fixedUpdateSystems.Add(new UnityEditor.EcsWorldDebugSystem());
                // Регистрируем отладочные системы по контролю за текущей группой систем. 
                _fixedUpdateSystems.Add(new UnityEditor.EcsSystemsDebugSystem());
#endif
            
            _fixedUpdateSystems.Inject();
            _fixedUpdateSystemState = _fixedUpdateSystems.SystemsState;
            AddToShare(_fixedUpdateSystems);
        }
        
        private void PrepareLateUpdateSystems()
        {
            _lateUpdateSystems = new EcsSystems(_world, _gameSharing);
            SetLateUpdateSystems(_lateUpdateSystems);
            _lateUpdateSystems.Inject();
            _lateUpdateSystemState = _lateUpdateSystems.SystemsState;
            AddToShare(_lateUpdateSystems);
        }
        
        private void OnDestroy() 
        {
            _coreSystems?.Destroy();
            _initSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _lateUpdateSystems?.Destroy();
        }
    }
}
