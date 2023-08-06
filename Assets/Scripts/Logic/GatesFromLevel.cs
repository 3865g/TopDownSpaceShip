using Scripts.Services.Input;
using Scripts.Services;
using Scripts.Services.StaticData;
using Scripts.StaticData;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Logic
{
    public class GatesFromLevel : MonoBehaviour
    {
        private IStaticDataService _staticDatatServiece;
        private LevelStaticData _levelStaticData;

        private void Awake()
        {
            _staticDatatServiece = AllServices.Container.Single<IStaticDataService>();
            _levelStaticData = _staticDatatServiece.ForLevel(SceneManager.GetActiveScene().name);
        }

        public void UpdateStatus()
        {
            foreach (EnemySpawnerStaticData enemySpawnerData in _levelStaticData.EnemySpawnerData)
            {

            }
        }
    }
}