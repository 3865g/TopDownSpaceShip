using Scripts.Enemy;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / AttackHPBalancer", order = 0)]
    public class AttackHPBalancer : SecondaryAbility
    {
        public int RestoreHP;
        public int DamageHP;

        private HeroHealth _heroHealth;
        private HeroAttack _heroAttack;
        private bool _damageChanged;

        public override void ActivatePassive(GameObject parent)
        {
            _heroHealth = parent.GetComponent<HeroHealth>();

            _heroHealth.HealthChanged += HPBalance;
            

        }

        public void HPBalance()
        {
            if (_heroHealth.CurrentHP < (_heroHealth.MaxHP / 2))
            {
                _heroHealth.TakeHP(RestoreHP);
            }
            else
            {
                _heroHealth.TakeDamage(DamageHP);
            }
        }
    }
}

