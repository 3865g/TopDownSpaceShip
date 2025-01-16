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

            if (secondaryAbility != null) 
            {
                _player.GetComponent<AbilityHolder>().ActivateSecondaryAbility(secondaryAbility);                
            }
            else
            {
                //Send coin
            }

            Destroy(gameObject);
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        public void FillingItem()
        {

            for (int i = 0; i < _rewardItem.Length; i++)
            {
                if (_secondaryAbilityService.ServiceSecondaryAbilities.Count > 0)
                {
                    _secondaryAbilityService.GetRandomSkill();
                    _rewardItem[i].NameAbility.SetText(_secondaryAbilityService.SecondaryAbility.name);
                    _rewardItem[i].DescriptionAbility.SetText(_secondaryAbilityService.SecondaryAbility.description);
                    _rewardItem[i].SecondaryAbility = _secondaryAbilityService.SecondaryAbility;
                }
                else
                {
                    _rewardItem[i].NameAbility.SetText("Take Coin");
                    _rewardItem[i].DescriptionAbility.SetText("Abilities is over");
                    _rewardItem[i].SecondaryAbility = null;
                }
                
            }


        }


    }
}
