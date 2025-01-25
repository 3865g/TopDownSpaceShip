using Scripts.Enemy;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Static Data/ Skill System / IncreaseAttribDamage", order = 0)]
    public class IncreaseAttribDammageAbility : SecondaryAbility
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

