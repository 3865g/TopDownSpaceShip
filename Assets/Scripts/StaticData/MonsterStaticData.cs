using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        
        public int Hp;
        public float Damage;

        public int MaxLoot;

        public int MinLoot;

        [Range(1f, 100f)]
        public float EffectiveDistane = 10f;

        [Range(1f, 100f)]
        public float Cleavage;

        [Range(1f, 100f)]
        public float MoveSpeed = 30f;


        public GameObject PrefabEnemy;
    }
}
