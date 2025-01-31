using Scripts.Hero.Ability;
using Scripts.Services.SecondaryAbilityService;
using Scripts.Services.StaticData;
using Scripts.StaticData;
using Scripts.UI.Services.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Logic
{
    public class RewardsManager : MonoBehaviour /*, ISavedProgress*/ 
    {
        public List<SecondaryAbility> RewardSecondaryAbilities;
        public List<SecondaryAbility> AllSecondaryAbilities = new List<SecondaryAbility>();
        public List<Rewards> rewardList = new List<Rewards>();
        public List<SecondaryAbility> AttributeAbilities;
       

        private IWindowService _windowService;
        private ISecondaryAbilityService _secondaryAbilityService;
        private IStaticDataService _staticDataService;

        public void Construct(IWindowService windowService, ISecondaryAbilityService secondaryAbilityService, IStaticDataService staticDataService)
        {
            _windowService = windowService;
            _secondaryAbilityService = secondaryAbilityService;
            _staticDataService = staticDataService;
            
            CreateLists();
        }


        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.M))
            {
                SendReward(2);
            }

            //NeedRefactoring
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (!_windowService.PauseMenu)
                {
                    _windowService.Open(WindowId.PauseMenu);
                }
                else
                {
                    Destroy(_windowService.PauseMenu.gameObject);
                }
                
            }
        }

        public void CreateLists()
        {
            CreateSecondAbilityList();
            CreateAttributeAbilityList();
        }

        public void CreateSecondAbilityList()
        {
            if (RewardSecondaryAbilities != null)
            {
                RewardSecondaryAbilities.Clear();
            }
            else
            {
                RewardSecondaryAbilities = new List<SecondaryAbility>();
            }

            foreach (SecondaryAbilityTypeId abilityKey in Enum.GetValues(typeof(SecondaryAbilityTypeId)))
            { 
            
                if(((int)abilityKey) < 55) 
                {
                    RewardSecondaryAbilities.Add(_staticDataService.ForSecondaryAbility(abilityKey));
                }
            }

            AllSecondaryAbilities.AddRange(RewardSecondaryAbilities);
            _secondaryAbilityService.SetAvailableAbilityList(RewardSecondaryAbilities);

        }

        public void CreateAttributeAbilityList()
        {
            if (AttributeAbilities != null)
            {
                AttributeAbilities.Clear();
            }
            else
            {
                AttributeAbilities = new List<SecondaryAbility>();
            }

            foreach (SecondaryAbilityTypeId abilityKey in Enum.GetValues(typeof(SecondaryAbilityTypeId)))
            {
                if (((int)abilityKey) > 54)
                {
                    AttributeAbilities.Add(_staticDataService.ForAttributeAbility(abilityKey));
                }
            }
            _secondaryAbilityService.SetAttributeAbilityList(AttributeAbilities);

        }

        public void RegisterEnemy(int groupId)
        {
            if (!rewardList.Any(x => x.GroupId == groupId))
            {
                rewardList.Add(new Rewards { GroupId = groupId, EnemyCount = 1 });
            }
            else
            {
                UpdateElement(groupId, 1);
            }


        }

        public void UpdateElement(int groupid, int enemyCount)
        {
            var element = rewardList.FirstOrDefault(x => x.GroupId == groupid);
            element.EnemyCount += enemyCount;

            if (element.EnemyCount == 0)
            {
                SendReward(groupid);
            }
        }

        public void UpdateEnemyCount(int groupId)
        {
            UpdateElement(groupId, -1);

        }

        public void SendReward(int groupid)
        {
            FillingAwards(groupid);
        }

        public void FillingAwards(int groupId)
        {
            
             
            if (groupId < 10)
            {
                _secondaryAbilityService.BoosLoot = false;
            }
            else
            {
                _secondaryAbilityService.BoosLoot = true;
            }

            //Play Reward sound??
            //Invoke("OpenWindow", 0.5f);

            OpenWindow();
        }

        public void OpenWindow()
        {
            _windowService.Open(WindowId.Rewards);
            Time.timeScale = 0f;
        }

        public void UpdateList(SecondaryAbility secondaryAbility)
        {
            RewardSecondaryAbilities.Remove(secondaryAbility);
            _secondaryAbilityService.SetAvailableAbilityList(RewardSecondaryAbilities);
        }
    }
}


