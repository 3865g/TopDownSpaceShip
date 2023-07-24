﻿using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Factory;
using Scripts.Services;
using Scripts.Services.PersistentProgress;
using Scripts.Services.SaveLoad;
using Scripts.Services.Input;
using Scripts.Services.Randomizer;
using UnityEngine;
using Scripts.Services.StaticData;
using Scripts.UI.Services.Factory;
using Scripts.UI.Services.Windows;
using System;
using CodeBase.Services.Ads;

namespace Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
                

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services) 
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }       

        private void RegisterServices()
        {
            RegisterStaticDataService();
            RegisterAdsService();

            _services.RegisterSingle<IGameStateMachine>(_gameStateMachine);
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>(), 
                _services.Single<IStaticDataService>(), 
                _services.Single<IPersistentProgressService>(),
                _services.Single<IAdsService>()));

            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));

            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), 
                _services.Single<IStaticDataService>(), 
                _services.Single<IRandomService>(), 
                _services.Single<IPersistentProgressService>(), 
                _services.Single<IWindowService>(),
                _services.Single<IGameStateMachine>()));

            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistentProgressService>(),
                _services.Single<IGameFactory>()));

        }

        private void RegisterAdsService()
        {
            IAdsService adsService = new AdsService();
            adsService.Initialize();
            _services.RegisterSingle<IAdsService>(adsService);
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            else
            {
                return new MobileInputService();
            }
        }

       

       
    }
}
    