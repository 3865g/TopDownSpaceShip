using System;
using UnityEngine;

namespace Scripts.StaticData
{
    [Serializable]
    public class EnemySpawnerStaticData
    {
        public string Id;
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;
        public int GroupId;
        public bool ThereAreAwards;

        public EnemySpawnerStaticData(string id, MonsterTypeId monsterTypeId, Vector3 position, int groupId, bool thereAreAwards)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
            GroupId = groupId;
            ThereAreAwards = thereAreAwards;
        }
    }
}
