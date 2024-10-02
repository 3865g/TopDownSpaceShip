using Scripts.Hero.Ability;
using Scripts.Services.SaveLoad;
using Scripts.Services;
using Scripts.UI.Windows.Menu;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class ChangeConfiguration : MonoBehaviour
    {
        public Button Button;
        public SkillType skillType;

        private AbilityManager _abilityManager;
        private ISaveLoadService _saveLoadService;

        

        private void OnEnable()
        {

            //Need move to gameFactory?
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

            Button.onClick.AddListener(ChangeShipConfiguration);
            LevelsMenu levelsMenu = GetComponentInParent<LevelsMenu>();
            _abilityManager = levelsMenu.AbilityManager;
        }

        public void ChangeShipConfiguration()
        {
            _abilityManager.ChangeConfiguration(Convert.ToInt32(skillType)); 
            
            _saveLoadService.SaveProgress();
        }
    }
}