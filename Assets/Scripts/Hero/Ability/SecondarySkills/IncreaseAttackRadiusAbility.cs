using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseAttackRadius", order = 0)]
    public class IncreaseAttackRadiusAbility : SecondaryAbility
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

