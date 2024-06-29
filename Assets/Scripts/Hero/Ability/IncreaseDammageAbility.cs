using Scripts.Enemy;
using Scripts.Hero.Ability;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseDamage", order = 0)]
    public class IncreaseDammageAbility : PassiveAbility
    {
        public int BonuseDamage;

        private HeroAttack _heroAttack;

        public override void ActivatePassive(GameObject parent)
        {
            _heroAttack = parent.GetComponent<HeroAttack>();
            _heroAttack.UptadeBonuseDamage(BonuseDamage);

        }
    }
}

