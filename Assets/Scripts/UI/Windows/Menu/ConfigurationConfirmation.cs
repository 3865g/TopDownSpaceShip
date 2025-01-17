using Scripts.Hero.Ability;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Menu
{

    public class ConfigurationConfirmation : MonoBehaviour
    {
        public GameObject ConfigurationIcon;
        public TextMeshProUGUI ConfigurationDescription;
        public TextMeshProUGUI OpeningConditions;
        public GameObject AbilityTier1Icon;
        public TextMeshProUGUI AbilityTier1Name;
        public TextMeshProUGUI AbilityTier1Description;
        public GameObject AbilityTier2Icon;
        public TextMeshProUGUI AbilityTier2Name;
        public TextMeshProUGUI AbilityTier2Description;
        public GameObject AbilityTier3Icon;
        public TextMeshProUGUI AbilityTier3Name;
        public TextMeshProUGUI AbilityTier3Description;


        private Sprite _configurationIcon;
        private Sprite _abilityTier1Icon;
        private Sprite _abilityTier2Icon;
        private Sprite _abilityTier3Icon;

        private void Awake()
        {
            SpriteVariable();
        }


        public void SpriteVariable()
        {
            _configurationIcon = ConfigurationIcon.GetComponent<Image>().sprite;
            _abilityTier1Icon = AbilityTier1Icon.GetComponent<Image>().sprite;
            _abilityTier2Icon = AbilityTier2Icon.GetComponent<Image>().sprite;
            _abilityTier3Icon = AbilityTier3Icon.GetComponent<Image>().sprite;
        }
        

        public void FillData(ConfigurationDescription configuration)
        {
            //_configurationIcon = configuration.ConfigurationIcon;
            //_abilityTier1Icon = configuration.AbilityTier1Icon;
            //_abilityTier2Icon= configuration.AbilityTier2Icon;
            //_abilityTier3Icon= configuration.AbilityTier3Icon;

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
