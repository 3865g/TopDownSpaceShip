using Scripts.Services.StaticData;
using Scripts.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Services.StaticData
{

    public class StaticDataService : IStaticDataService
    {
        private const string levelDataPath = "StaticData/Levels";
        private const string enemyDataPath = "StaticData/Enemy";
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;

        
        public void Load()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(enemyDataPath).ToDictionary(x => x.MonsterTypeId, x => x);
        
            _levels = Resources.LoadAll<LevelStaticData>(levelDataPath).ToDictionary(x => x.LevelKey, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId)
        {
            if (_monsters.TryGetValue(typeId, out MonsterStaticData monsterStaticData))
            {
                return monsterStaticData;
            }
            return null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData))
            {
                return levelStaticData;
            }
            return null;
        }
    }
}
