using Scripts.Hero.Ability;
using Scripts.Services.SecondaryAbilityService;
using Scripts.UI.Windows.Rewards;
using UnityEngine;

namespace Scripts.UI.Windows.Shop
{
    public class RewardWindow: WindowBase
    {
        
        private SecondaryAbility secondaryAbility;
        private ISecondaryAbilityService _secondaryAbilityService;
        private RewardItem[] _rewardItem;
        private GameObject _player;

        public void Construct(ISecondaryAbilityService secondaryAbilityService)
        {
            _secondaryAbilityService = secondaryAbilityService;
            _player = _secondaryAbilityService.Player;
            
        }

        private void Awake()
        {
            _rewardItem = GetComponentsInChildren<RewardItem>();
        }

        public void TakeAbility(SecondaryAbility secondaryAbility)
        {
            _player.GetComponent<AbilityHolder>().ActivateSecondaryAbility(secondaryAbility);

            Destroy(gameObject);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        public void FillingItem()
        {
            

            for(int i = 0; i < _rewardItem.Length; i++)
            {
                _secondaryAbilityService.GetRandomSkill();
                _rewardItem[i].NameAbility.SetText(_secondaryAbilityService.SecondaryAbility.name);
                _rewardItem[i].DescriptionAbility.SetText(_secondaryAbilityService.SecondaryAbility.description);
                _rewardItem[i].SecondaryAbility = _secondaryAbilityService.SecondaryAbility;
            }
        }


    }
}
