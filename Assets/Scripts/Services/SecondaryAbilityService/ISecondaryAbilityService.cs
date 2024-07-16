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
        AbilityManager AbilityManager { get; set; }
        void SetAvailableAbilityList(List<SecondaryAbility> secondaryAbilities);
        void GetRandomSkill();
    }
}
