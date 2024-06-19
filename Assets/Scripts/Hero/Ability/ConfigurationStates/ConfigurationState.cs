using Scripts.Hero.Ability;
using Scripts.Infrastructure.Factory;
using Scripts.Services.StaticData;
using Scripts.StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Hero.Ability.ConfigurationStattes
{

    public class ConfigurationState 
    {

        public virtual void Construct(IStaticDataService staticDataService, GameObject player)
        {
        }

        public virtual void InitActiveAbility()
        {
        }

        public virtual void ChangePoints(int attackPoints, int movementPoints, int defencePoints)
        {
        }

        public virtual void UnlockAbility()
        {

        }

        public virtual void SetActiveAbility()
        {
        }

    }

}
