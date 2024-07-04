using Scripts.Enemy;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / BurnShootsAbility", order = 0)]
    public class BurnShootsAbility : SecondaryAbility
    {
        public GameObject BurnLaserPrefab;

        private HeroAttack _heroAttack;

        public override void ActivatePassive(GameObject parent)
        {
            _heroAttack = parent.GetComponent<HeroAttack>();
            _heroAttack.Laserprefab = BurnLaserPrefab;

        }
    }
}

