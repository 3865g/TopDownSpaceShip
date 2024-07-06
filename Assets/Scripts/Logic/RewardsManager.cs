
using Scripts.Hero.Ability;
using Scripts.Services.StaticData;
using Scripts.StaticData;
using Scripts.UI.Services.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Logic
{
    public class RewardsManager : MonoBehaviour
    {
        public List<Rewards> rewardList = new List<Rewards>();
        public List<SecondaryAbility> secondaryAbilities = new List<SecondaryAbility>();

        private IWindowService _windowService;
        private IStaticDataService _staticDataService;
        private SecondaryAbilityTypeId _secondaryAbilityTypeId;
        private SecondaryAbility _secondaryAbility;

        public void Construct(IWindowService windowService, IStaticDataService staticDataService)
        {
            _windowService = windowService;
            _staticDataService = staticDataService;
            CreateAbilityList();
        }

        public void CreateWidget()
        {

        }


        public void CreateAbilityList()
        {

            foreach (SecondaryAbilityTypeId abilityKey in Enum.GetValues(typeof(SecondaryAbilityTypeId)))
            {
                secondaryAbilities.Add(_staticDataService.ForSecondaryAbility(abilityKey));
            }
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
            Time.timeScale = 0f;
             _windowService.Open(WindowId.Rewards);

            if (groupId < 10)
            {
                RandomChoosingSkill();
            }
            else
            {
                BoossRandomChoosingSkill();
            }
        }

        public void RandomChoosingSkill()
        {
            int randomIndex = new System.Random().Next(0, secondaryAbilities.Count);
            _secondaryAbility = secondaryAbilities[randomIndex];
            secondaryAbilities.Remove(_secondaryAbility);
        }

        public void BoossRandomChoosingSkill()
        {

            int points = 3;
            //var valuableAbilities = secondaryAbilities.Where(x => x.Point == points);

            var valuableAbilities = secondaryAbilities.Where(x => x.Point == points).ToList();

            if (valuableAbilities.Count() == 0 || points != 0)
            {
                points -= 1;
                valuableAbilities = secondaryAbilities.Where(x => x.Point == points).ToList();
            }
            else
            {
                Debug.LogError("You have all secondaryAbilities");
            }


            int randomValue = new System.Random().Next(0, valuableAbilities.Count());

            _secondaryAbility = valuableAbilities[randomValue];

            valuableAbilities.Remove(_secondaryAbility);
            secondaryAbilities.Remove(_secondaryAbility);


        }

    }
}


