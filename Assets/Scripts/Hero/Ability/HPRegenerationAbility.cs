using Scripts.Enemy;
using Scripts.Hero.Ability;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / HPRegenerationAbility", order = 0)]
    public class HPRegenerationAbility : PassiveAbility
    {
        public int RegenerationAmount;

        private HeroHealth _heroHealth;

        public override void ActivatePassive(GameObject parent)
        {
            _heroHealth = parent.GetComponent<HeroHealth>();

            //???
            _heroHealth.StartCoroutine(StartRegeneration()); 
            

        }

        private IEnumerator StartRegeneration()
        {
            while (true)
            {
                _heroHealth.CurrentHP += RegenerationAmount;

                yield return new WaitForSeconds(5);
            }

        }
    }
}

