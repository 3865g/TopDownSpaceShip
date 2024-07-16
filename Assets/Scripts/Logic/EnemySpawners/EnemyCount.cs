using Scripts.Infrastructure.Factory;
using Scripts.Logic.Gates;
using Scripts.Services;
using Scripts.Services.SaveLoad;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Logic.EnemySpawners
{
    public class EnemyCount : MonoBehaviour
    {
        public List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();

        public GameObject _gate;
        private GatesStatus _gateStatus;
        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory, GameObject gate)
        {
            _gameFactory = gameFactory;
            _gate = gate;
        }

        public void UpdateEnemyList(SpawnPoint spawnPoint)
        {
            SpawnPoints.Remove(spawnPoint);

            KillingEnemy();
        }

        private void KillingEnemy()
        {
            //_gate.GetComponent<GatesStatus>().UpdateStatus();
            //Debug.LogError(SpawnPoints.Count);
            if (SpawnPoints.Count <= 0)
            {
                _gate.GetComponent<GatesStatus>().UpdateStatus();
            }
        }
    }
}