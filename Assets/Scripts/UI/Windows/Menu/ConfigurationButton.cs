using Scripts.Hero.Ability;
using Scripts.Services.SaveLoad;
using Scripts.Services;
using Scripts.UI.Windows.Menu;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scripts.StaticData;
using Scripts.Services.StaticData;

namespace Scripts.UI.Windows.Rewards
{
    public class ConfigurationButton : MonoBehaviour
    {
        public Button Button;
        public TextMeshProUGUI NameConfiguration;
        public TextMeshProUGUI DescriptionConfiguration;

        public ConfigurationDescription Configuration;

        public ConfigurationTypeId ConfigurationTypeId;



        private AbilityManager _abilityManager;
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticDataService;



        private void Awake()
        {
            //Need move to gameFactory?

            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

            LevelsMenu levelsMenu = GetComponentInParent<LevelsMenu>();
            _abilityManager = levelsMenu.AbilityManager;
            _staticDataService = levelsMenu.StaticDataService;

            Configuration = _staticDataService.ForConfiguration(ConfigurationTypeId);
            FillText(Configuration);

            Button.onClick.AddListener(ChangeShipConfiguration);
        }

        public void FillText(ConfigurationDescription configuration)
        {
            NameConfiguration.SetText(configuration.name);
            DescriptionConfiguration.SetText(configuration.description);
        }

        public void ChangeShipConfiguration()
        {

            _abilityManager.ChangeConfiguration(Convert.ToInt32(Configuration.skillType));

            _saveLoadService.SaveProgress();
        }

    }
}

