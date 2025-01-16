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
        public TextMeshProUGUI NameAbility;
        public TextMeshProUGUI DescriptionAbility;

        public SecondaryAbility SecondaryAbility;

        [SerializeField] private Button TakeAbilityButton;



        private void Awake()
        {
            TakeAbilityButton.onClick.AddListener(() => ClickButton());
        }

        private void ClickButton()
        {
            RewardWindow rewardWindow = GetComponentInParent<RewardWindow>();
            rewardWindow.TakeAbility(SecondaryAbility);
        }
    }
}

