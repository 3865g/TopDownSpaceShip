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
        public void Construct(ISecondaryAbilityService secondaryAbilityService)
        {
            _secondaryAbilityService = secondaryAbilityService;
            
        }



        public void FillingItem()
        {
            _rewardItem = GetComponentsInChildren<RewardItem>();

            for(int i = 0; i < _rewardItem.Length; i++)
            {
                _secondaryAbilityService.GetRandomSkill();
                secondaryAbility = _secondaryAbilityService.secondaryAbility;
                _rewardItem[i].NameAbility.SetText(_secondaryAbilityService.secondaryAbility.name);
                _rewardItem[i].DescriptionAbility.SetText(_secondaryAbilityService.secondaryAbility.description);
            }
        }


    }
}
