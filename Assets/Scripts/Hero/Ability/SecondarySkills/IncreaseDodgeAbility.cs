using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / Dodge", order = 0)]
    public class IncreaseDodgeAbility : SecondaryAbility
    {
        public int DodgeChance;

        private HeroHealth _heroHealth;

        public override void ActivatePassive(GameObject parent)
        {
            _heroHealth = parent.GetComponent<HeroHealth>();
            if(_heroHealth.CanDodge == false)
            {
                _heroHealth.CanDodge = true;
            }
            _heroHealth.UpdateDodgeChance(DodgeChance);

        }
    }
}

