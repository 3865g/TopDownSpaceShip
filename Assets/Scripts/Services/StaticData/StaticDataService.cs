using Scripts.Hero.Ability;
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
        private const string _staticDataWindowPath = "StaticData/UI";
        private const string _staticDataHeroPath = "StaticData/Hero";
        private const string _staticDataMainAbilityPath = "ScriptableObjects/Skills/ConfigurationSkills";
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<GateTypeId, GateStaticData> _gate;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowStaticData> _windowConfigs;
        private Dictionary<HeroTyoeId,  HeroStaticData> _heroConfigs;
        private Dictionary<AbilityTypeId, Ability> _mainAbility;

        public void Load()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(_staticDataenemyPath).ToDictionary(x => x.MonsterTypeId, x => x);

            _gate = Resources.LoadAll<GateStaticData>(_staticDataSceneAssetsPath).ToDictionary(x => x.GateTypeId, x => x);
        
            _levels = Resources.LoadAll<LevelStaticData>(_staticDataLevelPath).ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources.LoadAll<WindowStaticData>(_staticDataWindowPath).ToDictionary(x => x.WindowId, x => x);

            _heroConfigs = Resources.LoadAll<HeroStaticData>(_staticDataHeroPath).ToDictionary(x => x.HeroTyoeId, x => x);

            _mainAbility = Resources.LoadAll<Ability>(_staticDataMainAbilityPath).ToDictionary(x => x.abilityTypeId, x => x);

            //Debug.Log(_windowConfigs);
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


        public WindowStaticData ForWindow(WindowId windowId)
        {
            if (_windowConfigs.TryGetValue(windowId, out WindowStaticData windowStaticData))
            {
                return windowStaticData;
            }
            return null;
        }

        public Ability ForAbility(AbilityTypeId abilityTypeId)
        {
            if (_mainAbility.TryGetValue(abilityTypeId, out Ability abilityStaticData))
            {
                return abilityStaticData;
            }
            return null;
        }

        public HeroStaticData ForHero(HeroTyoeId heroTypeId)
        {
            if (_heroConfigs.TryGetValue(heroTypeId, out HeroStaticData heroStaticData))
            {
                return heroStaticData;
            }
            return null;
        }
    }
}
