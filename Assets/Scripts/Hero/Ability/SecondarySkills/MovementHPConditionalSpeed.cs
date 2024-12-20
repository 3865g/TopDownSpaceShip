using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / MovementHPConditionalSpeed", order = 0)]
    public class MovementHPConditionalSpeed : SecondaryAbility
    {
        public int BonusSpeed;

        private HeroHealth _heroHealth;
        private ShipMove _shipMove;
        private bool _speedChanged;

        public override void ActivatePassive(GameObject parent)
        {
            _heroHealth = parent.GetComponent<HeroHealth>();
            _shipMove = parent.GetComponent<ShipMove>();

            ChangeDamage();

            _heroHealth.HealthChanged += ChangeDamage;


        }

        public void ChangeDamage()
        {
            if (_heroHealth.CurrentHP >= _heroHealth.MaxHP && !_speedChanged)
            {
                _shipMove.MovementSpeed += BonusSpeed;
                _speedChanged = true;
            }
            else if (_heroHealth.CurrentHP < _heroHealth.MaxHP && _speedChanged)
            {
                _shipMove.MovementSpeed -= BonusSpeed;
                _speedChanged = false;

            }
        }
    }
}

