using Scripts.Enemy;
using Scripts.Hero.Ability;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseMovementSpeed", order = 0)]
    public class IncreaseMovementSpeedAbility : PassiveAbility
    {
        public float BonuseSpeed;

        private ShipMove _shipMove;

        public override void ActivatePassive(GameObject parent)
        {
            _shipMove = parent.GetComponent<ShipMove>();
            _shipMove.UpdateBonuseSpeed(BonuseSpeed);

        }
    }
}

