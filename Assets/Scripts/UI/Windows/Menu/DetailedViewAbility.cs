using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Menu
{

    public class DetailedViewAbility : WindowBase
    {
        public Image Icon;
        public TextMeshProUGUI AbilityName;
        public TextMeshProUGUI AbilityDescription;

        public void Construct(Sprite sprite, string abilityName, string abilityDescription)
        {
            Icon.sprite = sprite;
            AbilityName.SetText(abilityName);
            AbilityDescription.SetText(abilityDescription);
        }
    }
}
