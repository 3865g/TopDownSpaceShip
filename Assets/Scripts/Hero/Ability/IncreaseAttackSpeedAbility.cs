using Scripts.Enemy;
using Scripts.Hero.Ability;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseAttackSpeed", order = 0)]
    public class IncreaseAttackSpeedAbility : PassiveAbility
    {
        public float AttackSpeedCooldown;

        private HeroAttack _heroAttack;

        public override void ActivatePassive(GameObject parent)
        {
            _heroAttack = parent.GetComponent<HeroAttack>();
            _heroAttack.UpdateAtackCooloduwn(AttackSpeedCooldown);

        }
    }
}

