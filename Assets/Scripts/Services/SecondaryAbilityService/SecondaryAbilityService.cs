using Scripts.Hero.Ability;
using System.Collections.Generic;
using System;

namespace Scripts.Services.SecondaryAbilityService
{
    public class SecondaryAbilityService : ISecondaryAbilityService
    {

        public List<SecondaryAbility> ServiceSecondaryAbilities { get; set; }
        public List<SecondaryAbility> CurrrentSecondaryAbilities { get; set; }
        public List<SecondaryAbility> AttributeAbilities { get; set; }
        public SecondaryAbility SecondaryAbility { get; set; }
        public SecondaryAbility AttributeAbility { get; set; }
        public AbilityManager AbilityManager { get; set; }

        public bool BoosLoot { get; set; }
        public UnityEngine.GameObject Player { get ; set ; }


        public void SetAvailableAbilityList(List<SecondaryAbility> secondaryAbilities)
        {
            if (CurrrentSecondaryAbilities != null)
            {
                CurrrentSecondaryAbilities.Clear();
            }
            else
            {
                CurrrentSecondaryAbilities = new List<SecondaryAbility>();
            }

            
            ServiceSecondaryAbilities = secondaryAbilities;
            CurrrentSecondaryAbilities.AddRange(ServiceSecondaryAbilities);
        }

        public void SetAttributeAbilityList(List<SecondaryAbility> secondaryAbilities)
        {
            if (AttributeAbilities != null)
            {
                AttributeAbilities.Clear();
            }
            else
            {
                AttributeAbilities = new List<SecondaryAbility>();
            }

            AttributeAbilities = secondaryAbilities;

        }

        public void GetRandomSkill()
        {

           

            if (CurrrentSecondaryAbilities.Count > 0)
            {
                int randomIndex = new Random().Next(0, CurrrentSecondaryAbilities.Count);
                AttributeAbility = null;
                SecondaryAbility = CurrrentSecondaryAbilities[randomIndex];
                CurrrentSecondaryAbilities.RemoveAt(randomIndex);
            }
            else
            {
                int randomIndex = new Random().Next(0, 2);
                SecondaryAbility = null;
                AttributeAbility = AttributeAbilities[randomIndex];
            }

            


            //switch (BoosLoot)
            //{
            //    case false:
            //        int randomIndex = new Random().Next(0, ServiceSecondaryAbilities.Count);
            //        SecondaryAbility = ServiceSecondaryAbilities[randomIndex];
            //        ServiceSecondaryAbilities.Remove(SecondaryAbility);
            //        break;

            //    case true:
            //        int points = 3;

            //        var valuableAbilities = ServiceSecondaryAbilities.Where(x => x.Point == points).ToList();

            //        if (valuableAbilities.Count() == 0 || points != 0)
            //        {
            //            points -= 1;
            //            valuableAbilities = ServiceSecondaryAbilities.Where(x => x.Point == points).ToList();
            //        }
            //        else
            //        {
            //            return;
            //        }

            //        int randomValue = new System.Random().Next(0, valuableAbilities.Count());
            //        SecondaryAbility = valuableAbilities[randomValue];
            //        valuableAbilities.Remove(SecondaryAbility);
            //        ServiceSecondaryAbilities.Remove(SecondaryAbility);

            //        break;


            //}


        }

    }
}
