using Scripts.Services;
using UnityEngine;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 spawnPosition);
        GameObject Instantiate(string path);
    }
}