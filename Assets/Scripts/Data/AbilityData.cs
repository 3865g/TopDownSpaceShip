using Scripts.Hero.Ability;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class AbilityData
    {
        public SkillType skillType;
        public int SkillTypeId;
        public int AttackPoints;
        public int MovementPoints;
        public int DefencePoints;
    }
}

