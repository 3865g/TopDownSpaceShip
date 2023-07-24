using Scripts.Enemy;
using Scripts.Services;
using Scripts.Services.PersistentProgress;
using Scripts.StaticData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        Task<GameObject> CreateHero(Vector3 playerInitialPoint);
        Task<GameObject> CreateHud();
        Task<GameObject> CreateEnemy(MonsterTypeId monsterTypeId, Transform parent);
        Task<LootPiece> CreateLoot();
        Task CreateSpawner(string spawnerId,Vector3 position, MonsterTypeId monsterTypeId);
        void Cleanup();
        Task WarmUp();
        Task CreateLevelTransfer(Vector3 transferInitialPoint);
    }
}