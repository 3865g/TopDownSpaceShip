using Scripts.Hero.Ability;
using Scripts.UI.Windows.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Rewards
{
    public class RewardItem : MonoBehaviour
    {
        public Button SelectAbilityButton;

        public Image Icon;
        public TextMeshProUGUI NameAbility;
        public TextMeshProUGUI DescriptionAbility;

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
            NameAbility.SetText(secondaryAbility.name);
            DescriptionAbility.SetText(secondaryAbility.description);
            SecondaryAbility = secondaryAbility;
            Icon.sprite = secondaryAbility.Icon;
            _outline.effectColor = secondaryAbility.Color;

        }

        public void FillingAttributeData(SecondaryAbility attributeAbility)
        {
            _secondary = false;
            NameAbility.SetText(attributeAbility.name);
            DescriptionAbility.SetText(attributeAbility.description);
            AttributeAbility = attributeAbility;
            Icon.sprite = attributeAbility.Icon;
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

