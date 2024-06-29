using Scripts.Enemy;
using Scripts.Hero.Ability;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / ReturnDamage", order = 0)]
    public class ReturnDamageAbility : PassiveAbility
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

