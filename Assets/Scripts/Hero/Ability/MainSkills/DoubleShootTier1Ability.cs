using Scripts.Logic;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "MainSkills / DoubleShoot", order = 0)]
    public class DoubleShootTier1Ability : ConfigurationAbility/*, ISavedProgress*/
    {
        public int BurstAmount;
        public float DamageMultiplier;

        private HeroAttack _heroAttack;
        private HeroHealth _health;

        public override void ActivatePassive(GameObject parent)
        {
            _heroAttack = parent.GetComponent<HeroAttack>();
            _heroAttack.BurstAmount = BurstAmount;

            _health = parent.GetComponent<HeroHealth>();  
            _health.DamageMultiplier = DamageMultiplier;
        }

        
    }
}

