using Scripts.Enemy;
using Scripts.Services;
using Scripts.Services.PersistentProgress;
using Scripts.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject CreateHero(Vector3 playerInitialPoint);
        GameObject CreateHud();
        GameObject CreateEnemy(MonsterTypeId monsterTypeId, Transform parent);
        LootPiece CreateLoot();
        void CreateSpawner(Vector3 position, string spawnerId, MonsterTypeId monsterTypeId);
        void CreateLevelTransfer(Vector3 transferInitialPoint);
        void Cleanup();
    }
}