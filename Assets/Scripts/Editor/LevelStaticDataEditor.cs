using Scripts.Logic;
using Scripts.Logic.EnemySpawners;
using Scripts.Logic.Gates;
using Scripts.StaticData;
using System.Linq;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string PlayerInitialPointTag = "PlayerInitialPoint";
        private const string LevelTransferInitialPoint = "LevelTransferInitialPoint";
        private const string LevelGateInitialPoint = "Gate";
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawnerData = FindObjectsOfType<SpawnMarker>().Select(x => new EnemySpawnerStaticData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position)).ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;

                levelData.InitialHeroPosition = GameObject.FindWithTag(PlayerInitialPointTag).transform.position;

                levelData.LevelTransfer.Position = GameObject.FindWithTag(LevelTransferInitialPoint).transform.position;

                levelData.LevelTransfer.TransferTo = GameObject.FindWithTag(LevelTransferInitialPoint).GetComponent<LevelTransferInitialPoint>().TransferTo;

                levelData.LevelGate.Position = GameObject.FindWithTag(LevelGateInitialPoint).transform.position;
                levelData.LevelGate.Rotation = GameObject.FindWithTag(LevelGateInitialPoint).transform.rotation;
                levelData.LevelGate.GateTypeId = GameObject.FindWithTag(LevelGateInitialPoint).GetComponent<GateSpawnMarker>().GateTypeId;
            }

            EditorUtility.SetDirty(target);
        }
    }
}
