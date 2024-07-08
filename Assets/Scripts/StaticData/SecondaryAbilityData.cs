using Scripts.Hero.Ability;
using System;
using System.Collections.Generic;

namespace Scripts.Data
{
    [Serializable]
    public class SecondaryAbilityData
    {
        public List<SecondaryAbility> AvailableAbilities;

        public Action ChangedValue;


        internal void Collect(SecondaryAbility addedAbility)
        {
            AvailableAbilities.Add(addedAbility);
            ChangedValue?.Invoke();
        }
    }
}
