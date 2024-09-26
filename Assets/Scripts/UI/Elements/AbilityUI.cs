using Scripts.Hero.Ability;
using UnityEngine;
using UnityEngine.UI;


namespace Scripts.UI.Elements
{
    public class AbilityUI : MonoBehaviour
    {
        public Button AbilityButtonUi;

        public AbilityButton AbilityButton;

        private AbilityHolder _abilityHolder;




        public void Construct(AbilityHolder abilityHolder)
        {
            _abilityHolder = abilityHolder;
        }


        private void Update()
        {
            //Need refactoring

            if (_abilityHolder)
            {
                UpdateButton();
            }     
        }


        public void UseAbility()
        {
            _abilityHolder.IsAbilityUse = true;
        }

        public void UpdateButton()
        {
            switch (_abilityHolder.CurrentAbilityState)
            {
                case 0:
                    break;

                case 1:
                    AbilityButton.ButtonActive(_abilityHolder.activeTime);
                    break;

                case 2:
                    AbilityButton.ButtoonCooldown(_abilityHolder.cooldownTime, _abilityHolder.activeAbility.ColdownTime);
                    break;


            }
        }
    }
}
