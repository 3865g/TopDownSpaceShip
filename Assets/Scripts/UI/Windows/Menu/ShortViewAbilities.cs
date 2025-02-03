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

        private Color _color;
        private string _abilityName;
        private string _abilityDescription;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService, Sprite icon, string abilityName, string abilityDescription, Color color)
        {
            _windowService = windowService;
            Icon.sprite = icon;
            _abilityName = abilityName;
            _abilityDescription = abilityDescription;
            _color = color;
            Button.onClick.AddListener(OpenDetailedDescription);

        }
        

        public void OpenDetailedDescription()
        {
            _windowService.Open(WindowId.DetailedViewAbilities);
            _windowService.DetailedViewAbility.Construct(Icon.sprite, _abilityName, _abilityDescription, _color);

        }
    }
}
