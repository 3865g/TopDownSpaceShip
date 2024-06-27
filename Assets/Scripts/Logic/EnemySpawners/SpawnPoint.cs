using Scripts.Data;
using Scripts.Enemy;
using Scripts.Infrastructure.Factory;
using Scripts.Services.PersistentProgress;
using Scripts.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        public EnemyCount EnemyCount;
        public RewardsManager RewardsManager;
        public string Id { get; set; }
        public bool _slain;
        public int GroupId;

        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;


        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        

        private void Start()
        {
            EnemyCount.SpawnPoints.Add(this);
            RewardsManager.RegisterEnemy(GroupId);
        }

        private void OnDestroy()
        {
            if(_enemyDeath != null)
            {
                _enemyDeath.Happened -= Slay;
            }
        }



        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
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
            List<string> slainSpawnerList = progress.KillData.ClearedSpawners;

            if (_slain && !slainSpawnerList.Contains(Id))
            {
                slainSpawnerList.Add(Id);
            }
        }
        private async void Spawn()
        {
           GameObject enemy = await _gameFactory.CreateEnemy(MonsterTypeId, transform);
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
            EnemyCount.UpdateEnemyList(this);
            RewardsManager.UpdateEnemyCount(GroupId);
        }
    }

}