using Scripts.Hero.Ability;
using Scripts.UI.Windows.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Rewards
{
    public class RewardItem : MonoBehaviour
    {
        public Button SelectAbilityButton;

        public Image Icon;
        public LocalizeStringEvent AbilityName;
        public LocalizeStringEvent AbilityDescription;

        private SecondaryAbility SecondaryAbility;
        private SecondaryAbility AttributeAbility;
        private Outline _outline;
        private bool _secondary;

        [SerializeField] private Button TakeAbilityButton;



        private void Awake()
        {
            TakeAbilityButton.onClick.AddListener(() => ClickButton());
            _outline = GetComponent<Outline>();
        }

        public void FillingSecondaryData(SecondaryAbility secondaryAbility)
        {
            _secondary = true;
            AbilityName.StringReference.TableEntryReference = secondaryAbility.AbilityName;
            AbilityDescription.StringReference.TableEntryReference = secondaryAbility.Description;
            SecondaryAbility = secondaryAbility;
            Icon.sprite = secondaryAbility.Icon;
            _outline.effectColor = secondaryAbility.Color;

            AbilityName.RefreshString();
            AbilityDescription.RefreshString();

        }

        public void FillingAttributeData(SecondaryAbility attributeAbility)
        {
            _secondary = false;
            AbilityName.StringReference.TableEntryReference = attributeAbility.AbilityName;
            AbilityDescription.StringReference.TableEntryReference = attributeAbility.Description;
            AttributeAbility = attributeAbility;
            Icon.sprite = attributeAbility.Icon;

            AbilityName.RefreshString();
            AbilityDescription.RefreshString();
        }

        private void ClickButton()
        {
            RewardWindow rewardWindow = GetComponentInParent<RewardWindow>();
            if (_secondary) 
            {
                rewardWindow.TakeSacondaryAbility(SecondaryAbility);
            }
            else
            {
                rewardWindow.TakeAttributeAbility(AttributeAbility);
            }
        }


    }
}

