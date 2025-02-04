using UnityEngine;
using Scripts.Infrastructure.AssetManagement;
using System.Collections.Generic;
using Scripts.Services.PersistentProgress;
using Scripts.StaticData;
using Object = UnityEngine.Object;
using Scripts.Logic;
using Scripts.UI.Elements;
using Scripts.Enemy;
using UnityEngine.AI;
using Scripts.Services.Randomizer;
using Scripts.Services.StaticData;
using Scripts.Logic.EnemySpawners;
using Assets.Scripts.UI.Elements;
using Scripts.UI.Services.Windows;
using Scripts.Infrastructure.States;
using System.Threading.Tasks;
using Scripts.Logic.Gates;
using Assets.Scripts.UI.Menu;
using Scripts.Hero.Ability;
using Scripts.Hero;
using Scripts.Services.SecondaryAbilityService;
using Scripts.Services.Ga;
using Scripts.Services.GameSettings;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedSettingsReader> SettingsReaders { get; } = new List<ISavedSettingsReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public List<ISavedSettings> SettingsWriters { get; } = new List<ISavedSettings>();



        private readonly IAssetProvider _assetsProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameSettingsService _gameSettingsService;
        private readonly IWindowService _windowService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISecondaryAbilityService _secondaryAbilityService;

        private GameObject _heroGameObject;
        private GameObject _gameManager;
        private GameObject _gate;
        private GameObject _hud;
        //Need refactoring
        private GameObject _abilityManager;
        

        public GameFactory(IAssetProvider assetsProvider,
            IStaticDataService staticDataService,
            IRandomService randomService,
            IPersistentProgressService persistentProgressService,
            IGameSettingsService gameSettingsService,
            IWindowService windowService,
            IGameStateMachine gameStateMachine,
            ISecondaryAbilityService secondaryAbilityService)
        {
            _assetsProvider = assetsProvider;
            _staticDataService = staticDataService;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
            _gameSettingsService = gameSettingsService;
            _windowService = windowService;
            _gameStateMachine = gameStateMachine;
            _secondaryAbilityService = secondaryAbilityService;
        }

        public async Task WarmUp()
        {
            await _assetsProvider.Load<GameObject>(AssetsAddress.Loot);
            await _assetsProvider.Load<GameObject>(AssetsAddress.Spawner);
        }


        public async Task CreateGameManager()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(AssetsAddress.GameManager);
            GameObject gameManager = Object.Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
            EnemyCount enemyCount = gameManager.GetComponent<EnemyCount>();
            enemyCount.Construct(_gate);

            RewardsManager rewardsManager = gameManager.GetComponent<RewardsManager>();
            rewardsManager.Construct(_windowService, _secondaryAbilityService, _staticDataService);            

            _gameManager = gameManager;
        }

     

        public async Task<GameObject> CreateHero(Vector3 playerInitialPoint)
        {
            HeroStaticData heroStaticData = _staticDataService.ForHero(HeroTyoeId.Hero1);

            _heroGameObject = await InstantiateRegisteredAsync(AssetsAddress.HeroPath, playerInitialPoint);

            HeroHealth health = _heroGameObject.GetComponent<HeroHealth>();
            health.Construct(_randomService);
            health.TextPrefab = await _assetsProvider.Load<GameObject>(AssetsAddress.HpText);
            HeroAttack heroAttack = _heroGameObject.GetComponent<HeroAttack>();
            heroAttack.Construct(_randomService);
            AbilityHolder abilityHolder = _heroGameObject.GetComponent<AbilityHolder>();
            abilityHolder.RewardsManager = _gameManager.GetComponent<RewardsManager>();
            abilityHolder.Construct(_abilityManager.GetComponent<AbilityManager>());
            HeroDeath heroDeath = _heroGameObject.GetComponent<HeroDeath>();
            heroDeath.Construct(_gameStateMachine, _windowService, _persistentProgressService);
            
            _abilityManager.GetComponent<AbilityManager>().InitPlayer(_heroGameObject, _persistentProgressService.Progress.AbilityProgress.SkillTypeId);

            _secondaryAbilityService.Player = _heroGameObject;
            //_hud.GetComponent<ActorUI>().Construct(_heroGameObject.GetComponent<IHealth>());

            return _heroGameObject;
        }

        public async Task CreateAbilityManager()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(AssetsAddress.AbilityManager);
            GameObject abilityManagerPrefab = InstantiateRegistered(prefab);
            AbilityManager abilityManager = abilityManagerPrefab.GetComponent<AbilityManager>();
            abilityManager.Construct(this, _staticDataService);


            _secondaryAbilityService.AbilityManager = abilityManager;

            _abilityManager = abilityManagerPrefab;
        }


        public async Task<GameObject> CreateHud()
        {

            _hud = await InstantiateRegisteredAsync(AssetsAddress.HudPath);

           // _hud.GetComponentInChildren<LootCounter>().Construct(_persistentProgressService.Progress.WorldData);

            foreach(OpenWindowButton openWindowButton in _hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }

            return _hud;
        }
        public async Task<GameObject> CreateMenu()
        {

            GameObject menu = await InstantiateRegisteredAsync(AssetsAddress.MenuPath);

           menu.GetComponentInChildren<MainMenu>().Construct(_gameStateMachine, _windowService);

            foreach(OpenWindowButton openWindowButton in menu.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }

            return menu;
        }

        public async Task CreateLevelTransfer(Vector3 transferInitialPoint, string transferTo)
        {
            GameObject prefab = await InstantiateRegisteredAsync(AssetsAddress.LevelTransferTrigger, transferInitialPoint);
            LevelTransferTrigger levelTransferTrigger = prefab.GetComponent<LevelTransferTrigger>();
            levelTransferTrigger.TransferTo = transferTo;
            //LevelTransferStaticData levelTransferStaticData = _staticDataService.
            levelTransferTrigger.Construct(_gameStateMachine);
        }

        public async Task CreateLevelGate(Vector3 position, Quaternion rotation, GateTypeId gateTypeId)
        {
            GateStaticData gateStaticData = _staticDataService.ForGate(gateTypeId);
            if(gateStaticData != null)
            {
                GameObject prefab = await _assetsProvider.Load<GameObject>(gateStaticData.PrefabGateReference);
                GameObject gate = Object.Instantiate(prefab, position, rotation);

                IGatesStatus gateStatus = prefab.GetComponent<IGatesStatus>();
                _gate = gate;
            }

        }

        public async Task<LootPiece> CreateLoot()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(AssetsAddress.Loot);
            LootPiece lootPiece = InstantiateRegistered(prefab).GetComponent<LootPiece>();
            if (_heroGameObject != null)
            {
                lootPiece.Construct(_persistentProgressService.Progress.WorldData, _heroGameObject, _gameManager.GetComponent<RewardsManager>());
            }
                

            return lootPiece;
        }

        public async Task<GameObject> CreateEnemy(MonsterTypeId monsterTypeId, Transform parent)
        {
            MonsterStaticData monsterStaticData = _staticDataService.ForMonster(monsterTypeId);

            GameObject prefab = await _assetsProvider.Load<GameObject>(monsterStaticData.PrefabEnemyReference);

            GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.CurrentHP = monsterStaticData.Hp;
            health.MaxHP = monsterStaticData.Hp;
            health.TextPrefab = await _assetsProvider.Load<GameObject>(AssetsAddress.HpText);


            //Need refactoring need hero transform from trigger?
            monster.GetComponent<AgentMoveToPlayer>()?.Construct(_heroGameObject);
            monster.GetComponent<AgentSearchPlayer>()?.Construct(_heroGameObject.transform);
            monster.GetComponent<RotateToHero>()?.Construct(_heroGameObject.transform);
            monster.GetComponentInChildren<RotateToHero>()?.Construct(_heroGameObject.transform);

            NavMeshAgent navMeshAgent = monster.GetComponent<NavMeshAgent>();
            if(navMeshAgent != null)
            {
                navMeshAgent.speed = monsterStaticData.MoveSpeed;
            }

            

            IAttack attack = monster.GetComponent<IAttack>();
            attack.Construct(_heroGameObject.transform, monsterStaticData.Damage);
            //attack.Damage = monsterStaticData.Damage;
            //attack.Cleavage = monsterStaticData.Cleavage;
            //attack.EffectiveDistane = monsterStaticData.EffectiveDistane;


            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterStaticData.MinLoot, monsterStaticData.MaxLoot);
            lootSpawner.Construct(this, _randomService);


            return monster;
        }

        public async Task  CreateSpawner(string spawnerId, Vector3 position, MonsterTypeId monsterTypeId, int groupid, bool thereAwaerds)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(AssetsAddress.Spawner);

            GameObject spawner = InstantiateRegistered(prefab, position);
            SpawnPoint spawnPoint = spawner.GetComponent<SpawnPoint>();

            spawnPoint.Construct(this);
            spawnPoint.Id = spawnerId;
            spawnPoint.MonsterTypeId = monsterTypeId;
            spawnPoint.EnemyCount = _gameManager.GetComponent<EnemyCount>();
            spawnPoint.RewardsManager = _gameManager.GetComponent<RewardsManager>();
            spawnPoint.GroupId = groupid;
            spawnPoint.ThereAreAwards = thereAwaerds;


        }



        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }

        public void RegisterSettings(ISavedSettingsReader settingsReader)
        {
            if (settingsReader is ISavedSettings settingsWriter)
            {
                SettingsWriters.Add(settingsWriter);
            }

            SettingsReaders.Add(settingsReader);
        }


        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }


        private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            GameObject gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 position)
        {
            GameObject gameObject = await _assetsProvider.Instantiate(prefabPath, spawnPosition: position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        } 
        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await _assetsProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            RegisterSettingsWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void RegisterSettingsWatchers(GameObject gameObject)
        {
            foreach (ISavedSettingsReader settingsReader in gameObject.GetComponentsInChildren<ISavedSettingsReader>())
            {
                RegisterSettings(settingsReader);
            }
        }


    }
}