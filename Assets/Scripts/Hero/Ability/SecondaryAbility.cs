using UnityEngine;
using Scripts.StaticData;

namespace Scripts.Hero.Ability
{
    public class SecondaryAbility : ScriptableObject
    {
        public string AbilityName;
        public Sprite Icon;
        public Color Color;
        public SecondaryAbilityTypeId  abilityTypeId;
        [TextArea (1, 4)]
        public string Description;
        public SkillType skillType;

        public bool ReActivateAfterLoad = true;

      
        public int Point;

       // public bool NotReActivate;
        
        public virtual void ActivatePassive(GameObject parent)
        {
        }

    }

}


