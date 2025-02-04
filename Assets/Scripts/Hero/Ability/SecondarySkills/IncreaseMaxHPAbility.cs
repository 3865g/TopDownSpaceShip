using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / AttribChangeMaxHP", order = 0)]
    public class IncreaseMaxHPAbility : SecondaryAbility
    {
        public int AddedMaxHP;

        private HeroHealth _heroHealth;

        public override void ActivatePassive(GameObject parent)
        {         
            _heroHealth = parent.GetComponent<HeroHealth>();

            _heroHealth.AddedBonusMaxHP(AddedMaxHP);
        }
    }
}

