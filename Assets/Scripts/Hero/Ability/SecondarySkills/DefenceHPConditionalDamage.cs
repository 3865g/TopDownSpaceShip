using Scripts.Enemy;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / DefenceHPConditionalDamage", order = 0)]
    public class DefenceHPConditionalDamage : SecondaryAbility
    {
        public int BonusDamage;

        private HeroHealth _heroHealth;
        private HeroAttack _heroAttack;
        private bool _damageChanged;

        public override void ActivatePassive(GameObject parent)
        {
            _heroHealth = parent.GetComponent<HeroHealth>();
            _heroAttack = parent.GetComponent<HeroAttack>();

            _heroHealth.HealthChanged += ChangeDamage;
            

        }

        public void ChangeDamage()
        {
            if (_heroHealth.CurrentHP >= _heroHealth.MaxHP)
            {
                if(_damageChanged == false)
                {
                    _heroAttack.BonuseDamage += BonusDamage;
                    _damageChanged = true;
                }
            }
            else
            {
                if (_damageChanged == true)
                {
                    _heroAttack.BonuseDamage -= BonusDamage;
                    _damageChanged = false;
                }
            }
        }
    }
}

