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
            //UpdateButtonIcon();
        }

        public void UpdateButtonIcon()
        {
            if (_abilityHolder.activeAbility != null)
            {
                AbilityButton.gameObject.SetActive(true);
                AbilityButton.AbilityImage.sprite = _abilityHolder.activeAbility.Icon;
            }
            else
            {
                AbilityButton.gameObject.SetActive(false);
            }
        }


        private void Update()
        {
            //Need refactoring

            if (_abilityHolder)
            {
                UpdateButton();
            }

            //if (_abilityHolder.activeAbility != null)
            //{
            //    if (AbilityButton.AbilityImage.sprite != _abilityHolder.activeAbility.Icon)
            //    {
            //        UpdateButtonIcon();
            //    }
            //}
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
