using Scripts.Hero.Ability;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Menu
{

    public class ConfigurationConfirmation : MonoBehaviour
    {
        public Image ConfigurationIcon;
        public Outline ConfigurationOutline;
        public TextMeshProUGUI ConfigurationDescription;
        public TextMeshProUGUI OpeningConditions;
        public Image AbilityTier1Icon;
        //public Outline AbilityTier1Outline;
        public TextMeshProUGUI AbilityTier1Name;
        public TextMeshProUGUI AbilityTier1Description;
        public Image AbilityTier2Icon;
        //public Outline AbilityTier2Outline;
        public TextMeshProUGUI AbilityTier2Name;
        public TextMeshProUGUI AbilityTier2Description;
        public Image AbilityTier3Icon;
        //public Outline AbilityTier3Outline;
        public TextMeshProUGUI AbilityTier3Name;
        public TextMeshProUGUI AbilityTier3Description;
        

        public void FillData(ConfigurationDescription configuration)
        {
            ConfigurationIcon.sprite = configuration.ConfigurationIcon;
            AbilityTier1Icon.sprite = configuration.AbilityTier1Icon;
            AbilityTier2Icon.sprite = configuration.AbilityTier2Icon;
            AbilityTier3Icon.sprite = configuration.AbilityTier3Icon;

            ConfigurationOutline.effectColor = configuration.Color;
            //AbilityTier1Outline.effectColor = configuration.Color;
            //AbilityTier2Outline.effectColor = configuration.Color;
            //AbilityTier3Outline.effectColor = configuration.Color;

            ConfigurationDescription.SetText(configuration.DetailedDescription);
            OpeningConditions.SetText(configuration.OpeningConditions);
            AbilityTier1Name.SetText(configuration.AbilityTier1Name);
            AbilityTier1Description.SetText(configuration.AbilityTier1Description);
            AbilityTier2Name.SetText(configuration.AbilityTier2Name);
            AbilityTier2Description.SetText(configuration.AbilityTier2Description);
            AbilityTier3Name.SetText(configuration.AbilityTier3Name);
            AbilityTier3Description.SetText(configuration.AbilityTier3Description);

        }

    }
}
