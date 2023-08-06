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
using System.Threading.Tasks;
using Scripts.Data;

namespace Scripts.Infrastructure.States
{

    public class LoadLevelState : IPayLoadedState<string>
    {
        
        private const string EnemySpawnerTag = "EnemySpawners";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, 
            SceneLoader sceneLoader, 
            LoadingCurtain curtain, 
            IGameFactory gameFactory, 
            IPersistentProgressService progressService, 
            IStaticDataService staticDataService, 
            IUIFactory uiFactory)
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
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }
        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitGameWorld();
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

        private async Task InitUIRoot()
        {
           await _uiFactory.CreateUIRoot();
        }


        private async Task InitGameWorld()
        {
            LevelStaticData levelStaticData = LevelStaticData();
            await InitGate(levelStaticData);
            await InitGameManager();
            await InitSpawners(levelStaticData);
            await InitLootPieces();
            GameObject hero = await InitHero(levelStaticData);
            await InitLevelTransfer(levelStaticData);
            await InitHud(hero);
            CameraFollow(hero);

        }

        private async Task InitGameManager()
        {
            await _gameFactory.CreateGameManager();
        }

        private async Task InitGate(LevelStaticData levelStaticData)
        {
            await _gameFactory.CreateLevelGate(levelStaticData.LevelGate.Position, levelStaticData.LevelGate.Rotation, levelStaticData.LevelGate.GateTypeId);
        }

        private async Task InitLevelTransfer(LevelStaticData levelStaticData)
        {
            await _gameFactory.CreateLevelTransfer(levelStaticData.LevelTransfer.Position, levelStaticData.LevelTransfer.TransferTo);
        }

        private async Task InitSpawners(LevelStaticData levelStaticData)
        {

            foreach(EnemySpawnerStaticData enemySpawnerData in levelStaticData.EnemySpawnerData)
            {
               await _gameFactory.CreateSpawner(enemySpawnerData.Id, enemySpawnerData.Position, enemySpawnerData.MonsterTypeId);
            }
        }

        private async Task InitLootPieces()
        {
            foreach(KeyValuePair <string, LootPieceData> item in _persistentProgressService.Progress.WorldData.LootData.LootPieceOnScene.Dictionary)
            {
                LootPiece lootPiece = await _gameFactory.CreateLoot();
                lootPiece.GetComponent<UniqueId>().Id = item.Key;
                lootPiece.Initialize(item.Value.Loot);
                lootPiece.transform.position = item.Value.Position.AsUnityVector();

            }
        }


        private async Task<GameObject> InitHero(LevelStaticData levelStaticData)
        {
            return await _gameFactory.CreateHero(levelStaticData.InitialHeroPosition);
        }

        private async Task InitHud(GameObject hero)
        {
            GameObject hud = await _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private LevelStaticData LevelStaticData()
        {
            return _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().FollowObject(hero);
        }

        
       
    }
}

