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
        private bool _activeAfterLoad;

        public override void ActivatePassive(GameObject parent)
        {
            _activeAfterLoad = false;
            _heroHealth = parent.GetComponent<HeroHealth>();
            _shipMove = parent.GetComponent<ShipMove>();
            FirstActivate();
            ChangeDamage();

            _heroHealth.HealthChanged += ChangeDamage;


        }

        public void ChangeDamage()
        {
            


            if (_heroHealth.CurrentHP >= _heroHealth.MaxHP && !_speedChanged)
            {
                _shipMove.UpdateBonuseSpeed(BonusSpeed);
                _speedChanged = true;
            }
            else if (_heroHealth.CurrentHP < _heroHealth.MaxHP && _speedChanged)
            {
                _shipMove.UpdateBonuseSpeed(-BonusSpeed);
                _speedChanged = false;

            }
        }

        public void FirstActivate()
        {
            if(!_activeAfterLoad && (_heroHealth.CurrentHP == _heroHealth.MaxHP))
            {
                _speedChanged = false;
                _activeAfterLoad = true;
            }
        }
    }
}

