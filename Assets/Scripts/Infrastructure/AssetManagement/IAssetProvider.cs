using Scripts.Services;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        Task<GameObject> Instantiate(string adress, Vector3 spawnPosition);
        Task<GameObject> Instantiate(string adress);

        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string adress) where T : class;

        void Cleanup();
        void Initialize();
    }
}