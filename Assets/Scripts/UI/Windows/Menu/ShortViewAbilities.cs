using Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Menu
{

    public class ShortViewAbilities : WindowBase
    {

        public Image Icon;
        public GameObject DetailedViewAbilities;
        public Button Button;


        private string _abilityName;
        private string _abilityDescription;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService, Sprite icon, string abilityName, string abilityDescription)
        {
            _windowService = windowService;
            Icon.sprite = icon;
            _abilityName = abilityName;
            _abilityDescription = abilityDescription;
            Button.onClick.AddListener(OpenDetailedDescription);

        }
        

        public void OpenDetailedDescription()
        {
            _windowService.Open(WindowId.DetailedViewAbilities);
            _windowService.DetailedViewAbility.Icon.sprite = Icon.sprite;
            _windowService.DetailedViewAbility.AbilityName.SetText(_abilityName);
            _windowService.DetailedViewAbility.AbilityDescription.SetText(_abilityDescription);

        }
    }
}
