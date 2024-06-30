using Scripts.Enemy;
using Scripts.Hero.Ability;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseAbilityCooldown", order = 0)]
    public class IncreaseAbilityCooldown : PassiveAbility
    {
        public float CooldownMultyplayer;

        private AbilityHolder _abilityHolder;

        public override void ActivatePassive(GameObject parent)
        {
            _abilityHolder = parent.GetComponent<AbilityHolder>();
            _abilityHolder.activeAbility.ColdownTime *=  CooldownMultyplayer; 
        }
    }
}

