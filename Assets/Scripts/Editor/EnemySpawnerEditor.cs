using Scripts.Logic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    internal class EnemySpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, 1f);
        }
    }
}
