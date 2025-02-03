using Scripts.Hero.Ability;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Menu
{

    public class ConfigurationConfirmation : MonoBehaviour
    {
        public Image ConfigurationIcon;
        public Outline ConfigurationOutline;
        public LocalizeStringEvent ConfigurationDescription;
        public LocalizeStringEvent OpeningConditions;
        public Image AbilityTier1Icon;
        //public Outline AbilityTier1Outline;
        public LocalizeStringEvent AbilityTier1Name;
        public LocalizeStringEvent AbilityTier1Description;
        public Image AbilityTier2Icon;
        //public Outline AbilityTier2Outline;
        public LocalizeStringEvent AbilityTier2Name;
        public LocalizeStringEvent AbilityTier2Description;
        public Image AbilityTier3Icon;
        //public Outline AbilityTier3Outline;
        public LocalizeStringEvent AbilityTier3Name;
        public LocalizeStringEvent AbilityTier3Description;
        

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

            ConfigurationDescription.StringReference.TableEntryReference = configuration.DetailedDescription;
            OpeningConditions.StringReference.TableEntryReference = configuration.OpeningConditions;
            AbilityTier1Name.StringReference.TableEntryReference = configuration.AbilityTier1Name;
            AbilityTier1Description.StringReference.TableEntryReference = configuration.AbilityTier1Description;
            AbilityTier2Name.StringReference.TableEntryReference = configuration.AbilityTier2Name;
            AbilityTier2Description.StringReference.TableEntryReference = configuration.AbilityTier2Description;
            AbilityTier3Name.StringReference.TableEntryReference = configuration.AbilityTier3Name;
            AbilityTier3Description.StringReference.TableEntryReference = configuration.AbilityTier3Description;


            ConfigurationDescription.RefreshString();
            OpeningConditions.RefreshString();
            AbilityTier1Name.RefreshString();
            AbilityTier1Description.RefreshString();
            AbilityTier2Name.RefreshString();
            AbilityTier2Description.RefreshString();
            AbilityTier3Name.RefreshString();
            AbilityTier3Description.RefreshString();

        }

    }
}
