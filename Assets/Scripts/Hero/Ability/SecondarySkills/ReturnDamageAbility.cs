using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / ReturnDamage", order = 0)]
    public class ReturnDamageAbility : SecondaryAbility
    {
        public int ReturnedDamage;

        private HeroHealth _heroHealth;

        public override void ActivatePassive(GameObject parent)
        {
            _heroHealth = parent.GetComponent<HeroHealth>();
           
            _heroHealth.ReturnDamage = true;
            _heroHealth.ReturnedDamage = ReturnedDamage;



        }
    }
}

