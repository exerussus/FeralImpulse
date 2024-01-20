﻿using System;
using System.Collections.Generic;
using Source.MonoBehaviours;
using UnityEngine;

namespace Source.EasyECS
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private List<EasyMonoBehaviour> _awake;
        [SerializeField] private List<EasyMonoBehaviour> _start;
        [SerializeField] private List<EasyMonoBehaviour> _share;

        [SerializeField] private List<DataPack> bootQueue;
        private Dictionary<Type, DataPack> _sharedData;
        [SerializeField] private GameShare gameShare;

        private void PreInit(EasyMonoBehaviour easyMonoBeh)
        {
            if (!_sharedData.ContainsKey(easyMonoBeh.GetType()))
            {
                var newPack = new DataPack(easyMonoBeh.GetType(), easyMonoBeh);
                easyMonoBeh.PreInit(gameShare, newPack);
                _sharedData[easyMonoBeh.GetType()] = newPack;

                if (easyMonoBeh is EcsStarter ecsStarter)
                {
                    ecsStarter.SetSharedData(gameShare);
                    ecsStarter.PreInit();
                }

                if (easyMonoBeh is Binder binder)
                {
                    //binder.PreInitialize();
                }
            }
        }

        private void PreInitAll()
        {
            _sharedData = new Dictionary<Type, DataPack>();
            bootQueue = new List<DataPack>();
            gameShare = new GameShare(_sharedData);

            foreach (var monoBeh in _share)
            {
                PreInit(monoBeh);
            }

            foreach (var monoBeh in _awake)
            {
                PreInit(monoBeh);
            }

            foreach (var monoBeh in _start)
            {
                PreInit(monoBeh);
            }

            foreach (var easyMonoBehaviour in _share) InjectDependencies(easyMonoBehaviour);
            foreach (var easyMonoBehaviour in _awake) InjectDependencies(easyMonoBehaviour);
            foreach (var easyMonoBehaviour in _start) InjectDependencies(easyMonoBehaviour);
        }

        private void Awake()
        {
            PreInitAll();

            foreach (var initMonoBeh in _awake)
            {
                initMonoBeh.Initialize();
                bootQueue.Add(_sharedData[initMonoBeh.GetType()]);
            }
        }

        private void Start()
        {
            foreach (var initMonoBeh in _start)
            {
                initMonoBeh.Initialize();
                bootQueue.Add(_sharedData[initMonoBeh.GetType()]);
            }
        }

        private void InjectDependencies(EasyMonoBehaviour easyMonoBehaviour)
        {
            easyMonoBehaviour.Inject();
        }
    }


    public class EasyInjectAttribute : PropertyAttribute
    {
    }
}