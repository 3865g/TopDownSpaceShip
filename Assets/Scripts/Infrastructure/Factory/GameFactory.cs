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
using Scripts.Services;
using Scripts.Services.Randomizer;
using Scripts.Services.StaticData;
using Scripts.Logic.EnemySpawners;
using Assets.Scripts.UI.Elements;
using Scripts.UI.Services.Windows;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private readonly IAssetProvider _assetsProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IWindowService _windowService;

        private GameObject _heroGameObject;

        public GameFactory(IAssetProvider assetsProvider, IStaticDataService staticDataService, IRandomService randomService, IPersistentProgressService persistentProgressService, IWindowService windowService)
        {
            _assetsProvider = assetsProvider;
            _staticDataService = staticDataService;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
            _windowService = windowService;
        }

      

        public GameObject CreateHero(GameObject playerInitialPoint)
        {
            _heroGameObject = InstantiateRegistered(AssetPath.HeroPath, playerInitialPoint.transform.position);
            return _heroGameObject;
        }


        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);
            hud.GetComponentInChildren<LootCounter>().Construct(_persistentProgressService.Progress.WorldData);

            foreach(OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }

            return hud;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot).GetComponent<LootPiece>();
            lootPiece.Construct(_persistentProgressService.Progress.WorldData);

            return lootPiece;
        }

        public GameObject CreateEnemy(MonsterTypeId monsterTypeId, Transform parent)
        {
            MonsterStaticData monsterStaticData = _staticDataService.ForMonster(monsterTypeId);
            GameObject monster = Object.Instantiate(monsterStaticData.PrefabEnemy, parent.position, Quaternion.identity, parent);

            var health = monster.GetComponent<IHealth>();
            health.CurrentHP = monsterStaticData.Hp;
            health.MaxHP = monsterStaticData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(_heroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterStaticData.MoveSpeed;

            

            var attack = monster.GetComponent<Attack>();
            attack.Construct(_heroGameObject.transform);
            attack.Damage = monsterStaticData.Damage;
            attack.Cleavage = monsterStaticData.Cleavage;
            attack.EffectiveDistane = monsterStaticData.EffectiveDistane;

            monster.GetComponent<RotateToHero>()?.Construct(_heroGameObject.transform);

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterStaticData.MinLoot, monsterStaticData.MaxLoot);
            lootSpawner.Construct(this, _randomService);


            return monster;
        }

        public void CreateSpawner(Vector3 position, string spawnerId, MonsterTypeId monsterTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, position).GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.Id = spawnerId;
            spawner.MonsterTypeId = monsterTypeId;
        }



        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }
        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }


        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _assetsProvider.Instantiate(path: prefabPath, spawnPosition: position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        } 
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assetsProvider.Instantiate(path: prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        
    }
}