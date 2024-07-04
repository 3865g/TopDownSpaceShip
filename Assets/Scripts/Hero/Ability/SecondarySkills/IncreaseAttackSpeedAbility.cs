using Scripts.Enemy;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseAttackSpeed", order = 0)]
    public class IncreaseAttackSpeedAbility : SecondaryAbility
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

