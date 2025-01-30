using Scripts.StaticData;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Configuration", menuName = "Configuration", order = 0)]

    public class ConfigurationDescription : ScriptableObject
    {
        public new string Name;
        public ConfigurationTypeId configurationTypeId;
        [TextArea(1, 4)]
        public string ShortDescription;
        public SkillType skillType;


        public Color Color;
        public Sprite ConfigurationIcon;
        [TextArea(1, 4)]
        public string DetailedDescription;
        [TextArea(1, 4)]
        public string OpeningConditions;
        public Sprite AbilityTier1Icon;
        [TextArea(1, 4)]
        public string AbilityTier1Name;
        [TextArea(1, 4)]
        public string AbilityTier1Description;
        public Sprite AbilityTier2Icon;
        [TextArea(1, 4)]
        public string AbilityTier2Name;
        [TextArea(1, 4)]
        public string AbilityTier2Description;
        public Sprite AbilityTier3Icon;
        [TextArea(1, 4)]
        public string AbilityTier3Name;
        [TextArea(1, 4)]
        public string AbilityTier3Description;

    }

}

