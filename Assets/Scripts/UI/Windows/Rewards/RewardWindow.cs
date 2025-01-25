using Scripts.Hero.Ability;
using Scripts.Services.SecondaryAbilityService;
using Scripts.UI.Windows.Rewards;
using UnityEngine;

namespace Scripts.UI.Windows.Shop
{
    public class RewardWindow : WindowBase
    {

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

        public void TakeSacondaryAbility(SecondaryAbility secondaryAbility)
        {

            if (secondaryAbility.skillType != SkillType.Attributes)
            {
                _player.GetComponent<AbilityHolder>().ActivateSecondaryAbility(secondaryAbility);
            }

            Destroy(gameObject);
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        public void TakeAttributeAbility(SecondaryAbility secondaryAbility)
        {

            if (secondaryAbility.skillType == SkillType.Attributes)
            {
                _player.GetComponent<AbilityHolder>().ActivateStatAbility(secondaryAbility);
            }

            Destroy(gameObject);
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        public void FillingItem()
        {
            ProcessingItems();
        }


        public void ProcessingItems()
        {
            for (int i = 0; i < _rewardItem.Length; i++)
            {
              
                    _secondaryAbilityService.GetRandomSkill();

                if(_secondaryAbilityService.SecondaryAbility != null)
                {
                    _rewardItem[i].FillingSecondaryData( _secondaryAbilityService.SecondaryAbility);
                }
                else if (_secondaryAbilityService.AttributeAbility != null)
                {
                    _rewardItem[i].FillingAttributeData( _secondaryAbilityService.AttributeAbility);
                }
                

            }
        }


    }
}
