using System.Collections.Generic;
using Scripts.Hero.Ability;
using UnityEngine;

namespace Scripts.Services.SecondaryAbilityService
{
    public interface ISecondaryAbilityService : IService
    {
        bool BoosLoot { get; set; }

        GameObject Player { get; set; }
        SecondaryAbility SecondaryAbility { get; set; }
        SecondaryAbility AttributeAbility { get; set; }
        List<SecondaryAbility> ServiceSecondaryAbilities { get; set; }
        List<SecondaryAbility> CurrrentSecondaryAbilities { get; set; }
        AbilityManager AbilityManager { get; set; }
        void SetAvailableAbilityList(List<SecondaryAbility> secondaryAbilities);
        void SetAttributeAbilityList(List<SecondaryAbility> secondaryAbilities);
        void GetRandomSkill();
    }
}
