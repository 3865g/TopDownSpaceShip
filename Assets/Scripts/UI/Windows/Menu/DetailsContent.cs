using Scripts.Hero.Ability;
using Scripts.Logic;
using Scripts.UI.Elements;
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

        public ShortViewAbilities Configuration;
        public ShortViewAbilities ConfigurationAbilityTier1;
        public ShortViewAbilities ConfigurationAbilityTier2;
        public ShortViewAbilities ConfigurationAbilityTier3;
        public ProgressBar ProgressBar;



        public GameObject _player;
        private AbilityHolder _abilityHolder;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService, GameObject player)
        {
            _windowService = windowService;
            _player = player;
            FillCurrentAbilityContainer();
            FillLibriaryAbilityContainer();
            FillCurrentConfigurationProgress();
        }


        public void FillCurrentAbilityContainer()
        {
            _abilityHolder = _player.GetComponent<AbilityHolder>();
            foreach (SecondaryAbility element in _abilityHolder.SecondaryAbilities)
            {
                ShortViewAbilities detailedViewAbility = GameObject.Instantiate(ShortAbilityView, CurrentAbilityContainer).GetComponent<ShortViewAbilities>();
                if (detailedViewAbility)
                {
                    detailedViewAbility.Construct(_windowService, element.Icon, element.name, element.description);
                }
            }
        }

        public void FillCurrentConfigurationProgress()
        {
            _abilityHolder = _player.GetComponent<AbilityHolder>();

            ProgressBar.SetValue(_abilityHolder.CurentPoints, 6);

            Configuration.Construct(_windowService, _abilityHolder.ConfigurationDescription.ConfigurationIcon, _abilityHolder.ConfigurationDescription.name, _abilityHolder.ConfigurationDescription.DetailedDescription);
            ConfigurationAbilityTier1.Construct(_windowService, _abilityHolder.ConfigurationDescription.AbilityTier1Icon, _abilityHolder.ConfigurationDescription.AbilityTier1Name, _abilityHolder.ConfigurationDescription.AbilityTier1Description);
            ConfigurationAbilityTier2.Construct(_windowService, _abilityHolder.ConfigurationDescription.AbilityTier2Icon, _abilityHolder.ConfigurationDescription.AbilityTier2Name, _abilityHolder.ConfigurationDescription.AbilityTier2Description);
            ConfigurationAbilityTier3.Construct(_windowService, _abilityHolder.ConfigurationDescription.AbilityTier3Icon, _abilityHolder.ConfigurationDescription.AbilityTier3Name, _abilityHolder.ConfigurationDescription.AbilityTier3Description);

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
