using Scripts.Hero.Ability;
using Scripts.Logic;
using TMPro;
using UnityEngine;
namespace Scripts.UI.Elements
{
    public class AbilityUI : MonoBehaviour
    {
        public HpBar HpBar;
        public TextMeshProUGUI TextMeshProUGUI;

        private AbilityHolder _abilityHolder;



        public void Construct(AbilityHolder abilityHolder)
        {
            _abilityHolder = abilityHolder;
        }
    }
}
