using Scripts.Data;
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

        private bool _slain;

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
        }

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }
    }
}