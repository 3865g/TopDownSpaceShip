
using Scripts.Hero.Ability;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Services.SecondaryAbilityService
{
    public class SecondaryAbilityService : ISecondaryAbilityService
    {

        public List<SecondaryAbility> SecondaryAbilities { get; set; }
        public SecondaryAbility SecondaryAbility { get; set; }
        public AbilityManager AbilityManager { get; set; }

        public bool BoosLoot { get; set; }
        public UnityEngine.GameObject Player { get ; set ; }

        public void SetAvailableAbilityList(List<SecondaryAbility> secondaryAbility)
        {
            SecondaryAbilities = secondaryAbility;
        }

        public void GetRandomSkill()
        {
            switch (BoosLoot)
            {
                case false:
                    int randomIndex = new Random().Next(0, SecondaryAbilities.Count);
                    SecondaryAbility = SecondaryAbilities[randomIndex];
                    SecondaryAbilities.Remove(SecondaryAbility);
                    break;

                case true:
                    int points = 3;

                    var valuableAbilities = SecondaryAbilities.Where(x => x.Point == points).ToList();

                    if (valuableAbilities.Count() == 0 || points != 0)
                    {
                        points -= 1;
                        valuableAbilities = SecondaryAbilities.Where(x => x.Point == points).ToList();
                    }
                    else
                    {
                        return;
                    }

                    int randomValue = new System.Random().Next(0, valuableAbilities.Count());
                    SecondaryAbility = valuableAbilities[randomValue];
                    valuableAbilities.Remove(SecondaryAbility);
                    SecondaryAbilities.Remove(SecondaryAbility);

                    break;


            }


        }

    }
}
