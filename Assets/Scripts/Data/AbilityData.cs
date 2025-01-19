using Scripts.Hero.Ability;
using Scripts.Hero.Ability.ConfigurationStattes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class AbilityData
    {
        public ConfigurationAbility ability;
        public SecondaryAbility secondaryAbility;
        public List<SecondaryAbility> secondaryAbilitiesData;

        public SkillType skillType;
        public int SkillTypeId;
        public int AttackPoints;
        public int MovementPoints;
        public int DefencePoints;
    }
}

