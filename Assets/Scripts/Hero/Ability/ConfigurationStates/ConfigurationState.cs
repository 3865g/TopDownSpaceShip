using Scripts.Services.StaticData;
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

        public virtual void SetPassiveAbility()
        {

        }
    }

}
