using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseAbilityCooldown", order = 0)]
    public class IncreaseAbilityCooldown : SecondaryAbility
    {
        public float CooldownMultyplayer;

        private AbilityHolder _abilityHolder;

        public override void ActivatePassive(GameObject parent)
        {
            _abilityHolder = parent.GetComponent<AbilityHolder>();
            _abilityHolder.activeAbility.ColdownTime *=  CooldownMultyplayer; 
        }
    }
}

