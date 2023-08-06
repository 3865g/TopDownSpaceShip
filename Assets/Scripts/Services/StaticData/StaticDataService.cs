using Scripts.Services.StaticData;
using Scripts.StaticData;
using Scripts.StaticData.Windows;
using Scripts.UI.Services.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Services.StaticData
{

    public class StaticDataService : IStaticDataService
    {
        private const string _staticDataLevelPath = "StaticData/Levels";
        private const string _staticDataSceneAssetsPath = "StaticData/SceneAssets";
        private const string _staticDataenemyPath = "StaticData/Enemy";
        private const string _staticDataWindowPath = "StaticData/UI/WindowStaticData";
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<GateTypeId, GateStaticData> _gate;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void Load()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(_staticDataenemyPath).ToDictionary(x => x.MonsterTypeId, x => x);

            _gate = Resources.LoadAll<GateStaticData>(_staticDataSceneAssetsPath).ToDictionary(x => x.GateTypeId, x => x);
        
            _levels = Resources.LoadAll<LevelStaticData>(_staticDataLevelPath).ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources.Load<WindowStaticData>(_staticDataWindowPath).Configs.ToDictionary(x => x.WindowId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId)
        {
            if (_monsters.TryGetValue(typeId, out MonsterStaticData monsterStaticData))
            {
                return monsterStaticData;
            }
            return null;
        }

        public GateStaticData ForGate(GateTypeId typeId)
        {
            if (_gate.TryGetValue(typeId, out GateStaticData gateStaticData))
            {
                return gateStaticData;
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


        public WindowConfig ForWindow(WindowId windowId)
        {
            if (_windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig))
            {
                return windowConfig;
            }
            return null;
        }

       
    }
}
