using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / ChangeMaxHP", order = 0)]
    public class IncreaseAttribMaxHPAbility : SecondaryAbility
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

