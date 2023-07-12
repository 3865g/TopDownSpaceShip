using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject HeroGameObject { get; }

        event Action HeroCreated;

        GameObject CreateHero(Vector3 playerInitialPoint);
        GameObject CreateHud();
        void Cleanup();
        void Register(ISavedProgressReader savedProgress);
    }
}