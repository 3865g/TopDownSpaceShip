using Scripts.CameraLogic;
using Scripts.Hero;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using Scripts.UI;
using System;
using UnityEngine;

namespace Scripts.Infrastructure.States
{

    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string PlayerInitialPointTag = "PlayerInitialPoint";
        private const string EnemySpawnerTag = "EnemySpawners";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly LoadingCurtain _curtain;
        private readonly IPersistentProgressService _persistentProgressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, Logic.LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _persistentProgressService = progressService;
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
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }       

        private void InitGameWorld()
        {
            InitSpawners();

            GameObject hero = InitHero();
            InitHud(hero);
            CameraFollow(hero);
        }

        

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private GameObject InitHero()
        {
            return _gameFactory.CreateHero(playerInitialPoint: GameObject.FindWithTag(PlayerInitialPointTag).transform.position);
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().FollowObject(hero);
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_persistentProgressService.Progress);
            }
        }
        private void InitSpawners()
        {
            foreach(GameObject spawnerObjects in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
               var spawner = spawnerObjects.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }
    }
}

