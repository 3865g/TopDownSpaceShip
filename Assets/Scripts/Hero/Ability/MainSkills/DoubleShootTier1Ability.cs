using Scripts.Enemy;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / DoubleShoot", order = 0)]
    public class DoubleShootTier1Ability : PassiveAbility
    {
        private HeroAttack _heroAttack;

        public override void ActivatePassive(GameObject parent)
        {
            _heroAttack = parent.GetComponent<HeroAttack>();
            _heroAttack.BurstAmount = 2;

        }
    }
}

