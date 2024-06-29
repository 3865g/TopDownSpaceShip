using Scripts.Enemy;
using Scripts.Hero.Ability;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseAttackRadius", order = 0)]
    public class IncreaseAttackRadiusAbility : PassiveAbility
    {
        public float AttackRadius;

        private TargetSearch _targetSearch;

        public override void ActivatePassive(GameObject parent)
        {
            _targetSearch = parent.GetComponent<TargetSearch>();
            _targetSearch.range += AttackRadius;
            

        }
    }
}

