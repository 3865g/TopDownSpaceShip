using UnityEngine;
using Scripts.StaticData;

namespace Scripts.Hero.Ability
{
    public class ConfigurationAbility : ScriptableObject
    {

        public new string name;
        public Sprite Icon;
        public AbilityTypeId abilityTypeId;
        [TextArea (1, 4)]
        public string description;
        public SkillType skillType;


        public float ColdownTime;
        public float ActiveTime;
        public int Cost;
        public int Point;
        public bool IsPassive;

        public virtual void Activate(GameObject parent)
        {
        }

        public virtual void Deactivate(GameObject parent) 
        {
        }
        public virtual void ActivatePassive(GameObject parent)
        {
        }

    }

}


