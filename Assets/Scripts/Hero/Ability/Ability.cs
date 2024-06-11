using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.StaticData;

namespace Scripts.Hero.Ability
{
    public class Ability : ScriptableObject
    {
        public new string name;
        public AbilityTypeId abilityTypeId;
        [TextArea (1, 4)]
        public string description;
        public SkillType skillType;


        public float ColdownTime;
        public float ActiveTime;
        public int Cost;

        public virtual void Activate(GameObject parent)
        {

        }

    }

}


