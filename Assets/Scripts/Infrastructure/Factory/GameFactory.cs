﻿using UnityEngine;
using Scripts.Infrastructure.AssetManagement;
using System.Collections.Generic;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.StaticData;
using Object = UnityEngine.Object;
using Scripts.Logic;
using Scripts.UI;
using Scripts.Enemy;
using UnityEngine.AI;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private readonly IAssetProvider _assetsProvider;
        private readonly IStaticDataService _staticDataService;

        private GameObject _heroGameObject;

        public GameFactory(IAssetProvider assetsProvider, IStaticDataService staticDataService)
        {
            _assetsProvider = assetsProvider;
            _staticDataService = staticDataService;
        }

        public GameObject CreateHero(GameObject playerInitialPoint)
        {
            _heroGameObject = InstantiateRegistered(AssetPath.HeroPath, playerInitialPoint.transform.position);
            return _heroGameObject;
        }


        public GameObject CreateHud()
        {
            return InstantiateRegistered(AssetPath.HudPath);
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


            return monster;
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