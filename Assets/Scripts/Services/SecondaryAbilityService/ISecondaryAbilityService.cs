using System.Collections.Generic;
using Scripts.Hero.Ability;

namespace Scripts.Services.SecondaryAbilityService
{
    public interface ISecondaryAbilityService : IService
    {
        bool BoosLoot { get; set; }
        SecondaryAbility secondaryAbility { get; set; }
        void Initialize(List<SecondaryAbility> secondaryAbilities);
        void GetRandomSkill();
    }
}
