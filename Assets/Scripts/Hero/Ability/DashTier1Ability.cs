using Scripts.Enemy;
using Scripts.Hero.Ability;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / DashTier1", order = 0)]
    public class DashTier1Ability : ActiveAbility
    {
        public float DashVelocity;

        private CharacterController _characterController;
        private ShipMove _shipMove;

        public override void Activate(GameObject parent)
        {

            if (!_characterController)
            {
                _characterController = parent.GetComponent<CharacterController>();
            }

            if (!_shipMove)
            {
                _shipMove = parent.GetComponent<ShipMove>();
            }

            _characterController.Move(_shipMove.MovementVector * DashVelocity);
        }

    }
}

