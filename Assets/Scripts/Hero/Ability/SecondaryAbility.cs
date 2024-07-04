using UnityEngine;
using Scripts.StaticData;

namespace Scripts.Hero.Ability
{
    public class SecondaryAbility : ScriptableObject
    {
        public new string name;
        public SecondaryAbilityTypeId  abilityTypeId;
        [TextArea (1, 4)]
        public string description;
        public SkillType skillType;


      
        public int Point;

        
        public virtual void ActivatePassive(GameObject parent)
        {
        }

    }

}


