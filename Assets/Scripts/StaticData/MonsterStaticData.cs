using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        [Range(1f, 100f)]
        public int Hp;

        [Range(1f, 100f)]
        public float Damage;

        [Range(1f, 100f)]
        public float EffectiveDistane = 10f;

        [Range(1f, 100f)]
        public float Cleavage;

        [Range(1f, 100f)]
        public float MoveSpeed = 30f;


        public GameObject PrefabEnemy;
    }
}
