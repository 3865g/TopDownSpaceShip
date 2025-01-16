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
        private const string _staticDataMainAbilityPath = "StaticData/Hero/Skills/ConfigurationSkills";
        private const string _staticDataSecondaryAbilityPath = "StaticData/Hero/Skills/SecondarySkills";
        private const string _staticDataConfigurationPath = "StaticData/Hero/Configurations";
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<GateTypeId, GateStaticData> _gate;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowStaticData> _windowConfigs;
        private Dictionary<HeroTyoeId, HeroStaticData> _heroConfigs;
        private Dictionary<AbilityTypeId, Ability> _mainAbilities;
        private Dictionary<SecondaryAbilityTypeId, SecondaryAbility> _secondaryAbilities;
        private Dictionary<ConfigurationTypeId, ConfigurationDescription> _configurations;

        public void Load()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(_staticDataenemyPath).ToDictionary(x => x.MonsterTypeId, x => x);

            _gate = Resources.LoadAll<GateStaticData>(_staticDataSceneAssetsPath).ToDictionary(x => x.GateTypeId, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(_staticDataLevelPath).ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources.LoadAll<WindowStaticData>(_staticDataWindowPath).ToDictionary(x => x.WindowId, x => x);

            _heroConfigs = Resources.LoadAll<HeroStaticData>(_staticDataHeroPath).ToDictionary(x => x.HeroTyoeId, x => x);

            _mainAbilities = Resources.LoadAll<Ability>(_staticDataMainAbilityPath).ToDictionary(x => x.abilityTypeId, x => x);

            _secondaryAbilities = Resources.LoadAll<SecondaryAbility>(_staticDataSecondaryAbilityPath).ToDictionary(x => x.abilityTypeId, x => x);

            _configurations = Resources.LoadAll<ConfigurationDescription>(_staticDataConfigurationPath).ToDictionary(x => x.configurationTypeId, x => x);

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
            if (_mainAbilities.TryGetValue(abilityTypeId, out Ability abilityStaticData))
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

        public SecondaryAbility ForSecondaryAbility(SecondaryAbilityTypeId abilityTypeId)
        {
            if (_secondaryAbilities.TryGetValue(abilityTypeId, out SecondaryAbility secondaryAbilityStaticData))
            {
                return secondaryAbilityStaticData;
            }
            return null;
        }

        public ConfigurationDescription ForConfiguration(ConfigurationTypeId configurationTypeId)
        {
            if (_configurations.TryGetValue(configurationTypeId, out ConfigurationDescription configurationStaticData))
            {
                return configurationStaticData;
            }
            return null;
        }
    }
}
