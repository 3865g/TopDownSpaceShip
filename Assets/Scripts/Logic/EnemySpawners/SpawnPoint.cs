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
        public string Id { get; set; }
        private IGameFactory _gameFactory;
        private bool _slain;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
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