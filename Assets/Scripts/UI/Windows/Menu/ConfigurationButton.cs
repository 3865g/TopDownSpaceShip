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
using UnityEngine.Localization.Components;

namespace Scripts.UI.Windows.Rewards
{
    public class ConfigurationButton : MonoBehaviour
    {
        public Button Button;
        public Image Icon;
        //public TextMeshProUGUI NameConfiguration;
        //public TextMeshProUGUI DescriptionConfiguration;
        public LocalizeStringEvent NameConfiguration;
        public LocalizeStringEvent DescriptionConfiguration;


        public ConfigurationDescription Configuration;
        public ConfigurationConfirmation ConfigurationConfirmation;

        public ConfigurationTypeId ConfigurationTypeId;



        private Outline _outline;
        private AbilityManager _abilityManager;
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticDataService;



        private void Awake()
        {
            //Need move to gameFactory?

            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

            LevelsMenu levelsMenu = GetComponentInParent<LevelsMenu>();
            _outline = GetComponent<Outline>();
            _abilityManager = levelsMenu.AbilityManager;
            _staticDataService = levelsMenu.StaticDataService;

            Configuration = _staticDataService.ForConfiguration(ConfigurationTypeId);
            FillData(Configuration);

            Button.onClick.AddListener(ChangeShipConfiguration);
        }

        public void FillData(ConfigurationDescription configuration)
        {
            Icon.sprite = configuration.ConfigurationIcon;
            NameConfiguration.StringReference.TableEntryReference = configuration.ConfigurationName;
            DescriptionConfiguration.StringReference.TableEntryReference = configuration.ShortDescription;
            DescriptionConfiguration.RefreshString();
            NameConfiguration.RefreshString();
            _outline.effectColor = configuration.Color;
        }

        public void ChangeShipConfiguration()
        {

            _abilityManager.ChangeConfiguration(Convert.ToInt32(Configuration.skillType));

            _saveLoadService.SaveProgress();

            ConfigurationConfirmation.FillData(Configuration);
        }

    }
}

