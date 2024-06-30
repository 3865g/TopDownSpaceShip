using Scripts.Enemy;
using Scripts.Hero.Ability;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseCriticalChance", order = 0)]
    public class IncreaseCriticalChanceAbility : PassiveAbility
    {
        public int CriticalChance;
        public int CriticalDamage;

        private HeroAttack _heroAttack;

        public override void ActivatePassive(GameObject parent)
        {
            _heroAttack = parent.GetComponent<HeroAttack>();
            _heroAttack.CriticalChance = CriticalChance;
            _heroAttack.CriticalDamage = CriticalDamage;

        }
    }
}

