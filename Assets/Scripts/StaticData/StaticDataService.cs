using Scripts.Services.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.StaticData
{

    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("StaticData/Enemy").ToDictionary(x => x.MonsterTypeId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId)
        {
            if (_monsters.TryGetValue(typeId, out MonsterStaticData monsterStaticData))
            {
                return monsterStaticData;
            }
            return null;
        }
    }
}
