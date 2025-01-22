using Scripts.Hero.Ability;
using Scripts.Logic;
using Scripts.UI.Services.Windows;
using UnityEngine;

namespace Scripts.UI.Windows.Menu
{

    public class DetailsContent : WindowBase
    {
        public GameObject ShortAbilityView;

        public Transform CurrentAbilityContainer;

        public Transform AttackingLibriaryContainer;
        public Transform MovementLibriaryContainer;
        public Transform ProtectiveLibriaryContainer;

        public GameObject _player;
        private AbilityHolder _abilityHolder;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService, GameObject player)
        {
            _windowService = windowService;
            _player = player;
            FillCurrentAbilityContainer();
            FillLibriaryAbilityContainer();
        }


        public void FillCurrentAbilityContainer()
        {
            _abilityHolder = _player.GetComponent<AbilityHolder>();
            foreach (SecondaryAbility element in _abilityHolder.secondaryAbilities)
            {
                ShortViewAbilities detailedViewAbility = GameObject.Instantiate(ShortAbilityView, CurrentAbilityContainer).GetComponent<ShortViewAbilities>();
                if (detailedViewAbility)
                {
                    detailedViewAbility.Construct(_windowService, element.Icon, element.name, element.description);
                }
            }
        }

        public void FillLibriaryAbilityContainer()
        {
            RewardsManager rewardsManager = _player.GetComponent<AbilityHolder>().RewardsManager;
            
            foreach(SecondaryAbility element in rewardsManager.AllSecondaryAbilities)
            {
                switch (element.skillType)
                {
                    case SkillType.Attacking:
                        ShortViewAbilities attackingDetailedViewAbility = GameObject.Instantiate(ShortAbilityView, AttackingLibriaryContainer).GetComponent<ShortViewAbilities>();
                        if (attackingDetailedViewAbility)
                        {
                            attackingDetailedViewAbility.Construct(_windowService, element.Icon, element.name, element.description);
                        }
                        break;
                    case SkillType.Movement:
                        ShortViewAbilities movementDetailedViewAbility = GameObject.Instantiate(ShortAbilityView, MovementLibriaryContainer).GetComponent<ShortViewAbilities>();
                        if (movementDetailedViewAbility)
                        {
                            movementDetailedViewAbility.Construct(_windowService, element.Icon, element.name, element.description);
                        }
                        break;
                    case SkillType.Protective:
                        ShortViewAbilities protectiveDetailedViewAbility = GameObject.Instantiate(ShortAbilityView, ProtectiveLibriaryContainer).GetComponent<ShortViewAbilities>();
                        if (protectiveDetailedViewAbility)
                        {
                            protectiveDetailedViewAbility.Construct(_windowService, element.Icon, element.name, element.description);
                        }
                        break;


                }
            }

        }

        
    }
}
