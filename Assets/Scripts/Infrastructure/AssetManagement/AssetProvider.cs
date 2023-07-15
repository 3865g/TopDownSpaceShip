using UnityEngine;

namespace Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path, Vector3 spawnPosition)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

    }
}