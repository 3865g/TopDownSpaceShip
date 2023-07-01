using UnityEngine;
using Scripts.Infrastructure.AssetManagement;
using System.Collections.Generic;
using Scripts.Infrastructure.Services.PersistentProgress;
using System;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {

        private IAssetProvider _assetsProvider;

        

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }

        public event Action HeroCreated;

        public GameFactory(IAssetProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public GameObject CreateHero(Vector3 playerInitialPoint)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, playerInitialPoint);
            HeroCreated?.Invoke();
            return HeroGameObject;
        }


        public void CreateHud()
        {
            InstantiateRegistered(AssetPath.HudPath);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }
        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _assetsProvider.Instantiate(prefabPath, position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        } 
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assetsProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        
    }
}