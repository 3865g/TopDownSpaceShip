using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
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

        GameObject CreateHero(GameObject playerInitialPoint);
        GameObject CreateHud();
        GameObject CreateEnemy(MonsterTypeId monsterTypeId, Transform parent);
        void Cleanup();
        void Register(ISavedProgressReader savedProgress);
    }
}