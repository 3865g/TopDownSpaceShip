using Scripts.Logic;
using Scripts.Logic.EnemySpawners;
using Scripts.StaticData;
using System.Linq;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    internal class LevelStaticDataEditor : UnityEditor.Editor
    {
       public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawnerData = FindObjectsOfType<SpawnMarker>().Select(x => new EnemySpawnerStaticData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position)).ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }

            EditorUtility.SetDirty(target);
        }
    }
}
