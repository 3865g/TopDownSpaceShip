using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;

        public List<EnemySpawnerStaticData> EnemySpawnerData;

        public Vector3 InitialHeroPosition;
        public LevelTransferStaticData LevelTransfer;
        public GateSpawnerData LevelGate;
    }
}
