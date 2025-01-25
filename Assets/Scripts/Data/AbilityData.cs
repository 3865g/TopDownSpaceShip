using Scripts.Hero.Ability;
using System;
using System.Collections.Generic;

namespace Scripts.Data
{
    [Serializable]
    public class AbilityData
    {
        public ConfigurationAbility ability;
        public SecondaryAbility secondaryAbility;
        public List<SecondaryAbility> secondaryAbilitiesData;
        public List<SecondaryAbility> attributeAbilitiesData;

        public SkillType skillType;
        public int SkillTypeId;
        public int AttackPoints;
        public int MovementPoints;
        public int DefencePoints;
    }
}

