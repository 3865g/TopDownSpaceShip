using Scripts.Hero.Ability;
using Scripts.UI.Services.Windows;
using UnityEngine;

namespace Scripts.UI.Windows.Menu
{

    public class DetailsContent : WindowBase
    {
        public GameObject ShortAbilityView;
        public Transform ShortAbilityContainer;
        public GameObject _player;
        private AbilityHolder _abilityHolder;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService, GameObject player)
        {
            _windowService = windowService;
            _player = player;
            FillContainer();
        }


        public void FillContainer()
        {
            _abilityHolder = _player.GetComponent<AbilityHolder>();
            foreach (SecondaryAbility element in _abilityHolder.secondaryAbilities)
            {
                ShortViewAbilities detailedViewAbility = GameObject.Instantiate(ShortAbilityView, ShortAbilityContainer).GetComponent<ShortViewAbilities>();
                if (detailedViewAbility)
                {
                    detailedViewAbility.Construct(_windowService, element.Icon, element.name, element.description);
                }
            }
        }

        
    }
}
