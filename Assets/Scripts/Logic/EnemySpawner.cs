using Scripts.Data;
using Scripts.Enemy;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.StaticData;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        private string _id;
        private IGameFactory _gameFactory;
        private bool _slain;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
            {
                _slain = true;
            }
            else
            {
                Spawn();
            }
        }


        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
            {
                progress.KillData.ClearedSpawners.Add(_id);
            }
        }
        private void Spawn()
        {
           GameObject enemy = _gameFactory.CreateEnemy(MonsterTypeId, transform);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if(_enemyDeath != null)
            {
                _enemyDeath.Happened -= Slay;
            }
            _slain = true;
        }
    }
}