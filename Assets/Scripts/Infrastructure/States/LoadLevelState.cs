using System.Collections.Generic;
using Scripts.Services.PersistentProgress;
using Scripts.CameraLogic;
using Scripts.Enemy;
using Scripts.Hero;
using Scripts.Infrastructure.Factory;
using Scripts.Logic;
using Scripts.UI.Elements;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.StaticData;
using Scripts.Services.StaticData;
using System;
using Scripts.UI.Services.Factory;

namespace Scripts.Infrastructure.States
{

    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string PlayerInitialPointTag = "PlayerInitialPoint";
        private const string EnemySpawnerTag = "EnemySpawners";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _persistentProgressService = progressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, onLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }
        private void onLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }
        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_persistentProgressService.Progress);
            }
        }

        private void InitUIRoot()
        {
            _uiFactory.CreateUIRoot();
        }


        private void InitGameWorld()
        {
            InitSpawners();
            InitLootPieces();
            GameObject hero = InitHero();
            InitHud(hero);
            CameraFollow(hero);

        }
        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelStaticData = _staticDataService.ForLevel(sceneKey);

            foreach(EnemySpawnerStaticData enemySpawnerData in levelStaticData.EnemySpawnerData)
            {
                _gameFactory.CreateSpawner(enemySpawnerData.Position, enemySpawnerData.Id, enemySpawnerData.MonsterTypeId);
            }
        }

        private void InitLootPieces()
        {
            foreach(string key in _persistentProgressService.Progress.WorldData.LootData.LootPieceOnScene.Dictionary.Keys)
            {
                LootPiece lootPiece = _gameFactory.CreateLoot();
                lootPiece.GetComponent<UniqueId>().Id = key;
            }
        }


        private GameObject InitHero()
        {
            return _gameFactory.CreateHero(GameObject.FindWithTag(PlayerInitialPointTag));
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        

        private void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().FollowObject(hero);
        }

        
       
    }
}

