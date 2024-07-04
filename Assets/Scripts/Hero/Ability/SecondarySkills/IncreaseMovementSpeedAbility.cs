using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / IncreaseMovementSpeed", order = 0)]
    public class IncreaseMovementSpeedAbility : SecondaryAbility
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

