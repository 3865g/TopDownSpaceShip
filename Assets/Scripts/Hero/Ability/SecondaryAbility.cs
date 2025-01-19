using UnityEngine;
using Scripts.StaticData;

namespace Scripts.Hero.Ability
{
    public class SecondaryAbility : ScriptableObject
    {
        public new string name;
        public Sprite Icon;
        public SecondaryAbilityTypeId  abilityTypeId;
        [TextArea (1, 4)]
        public string description;
        public SkillType skillType;

        public bool ReActivateAfterLoad = true;

      
        public int Point;

       // public bool NotReActivate;
        
        public virtual void ActivatePassive(GameObject parent)
        {
        }

    }

}


