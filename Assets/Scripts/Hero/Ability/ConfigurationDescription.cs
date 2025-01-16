using Scripts.StaticData;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Configuration", menuName = "Configuration", order = 0)]

    public class ConfigurationDescription : ScriptableObject
    {
        public new string name;
        public ConfigurationTypeId configurationTypeId;
        [TextArea(1, 4)]
        public string description;
        public SkillType skillType;
    }

}

