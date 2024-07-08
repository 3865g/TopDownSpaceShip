
using Scripts.Hero.Ability;
using Scripts.Services.StaticData;
using Scripts.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Services.SecondaryAbilityService
{
    public class SecondaryAbilityService : ISecondaryAbilityService
    {

        public List<SecondaryAbility> SecondaryAbilities { get; set; }
        public SecondaryAbility secondaryAbility { get; set; }

        public bool BoosLoot { get; set; }




        public void Initialize(List<SecondaryAbility> secondaryAbility)
        {
            SecondaryAbilities = secondaryAbility;
        }

        public void GetRandomSkill()
        {
            switch (BoosLoot)
            {
                case false:
                    int randomIndex = new Random().Next(0, SecondaryAbilities.Count);
                    secondaryAbility = SecondaryAbilities[randomIndex];
                    SecondaryAbilities.Remove(secondaryAbility);
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
                    secondaryAbility = valuableAbilities[randomValue];
                    valuableAbilities.Remove(secondaryAbility);
                    SecondaryAbilities.Remove(secondaryAbility);

                    break;


            }


        }

    }
}
