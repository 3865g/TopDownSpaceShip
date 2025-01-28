using Scripts.Services.StaticData;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Hero.Ability.ConfigurationStattes
{


    public class AttackConfiguration : ConfigurationState
    {

        public AbilityTypeId abilityTypeId;

        private ConfigurationAbility _activeAbility;
        private ConfigurationAbility _abilityTier1;
        private ConfigurationAbility _abilityTier2;
        private ConfigurationAbility _abilityTier3;

        private GameObject _player;
        private IStaticDataService _staticDataService;

        private int _attackPoints;
        private int _movementPoints;
        private int _defencePoints;


        public override void Construct(IStaticDataService staticDataService, GameObject player)
        {

            _staticDataService = staticDataService;
            _player = player;
        }

        public override void InitActiveAbility()
        {

            _abilityTier1 = _staticDataService.ForAbility(AbilityTypeId.DoubleShootTier1);
            _abilityTier2 = _staticDataService.ForAbility(AbilityTypeId.DoubleShootTier2);
            _abilityTier3 = _staticDataService.ForAbility(AbilityTypeId.DoubleShootTier3);

            UnlockAbility();
        }

        public override void ChangePoints(int attackPoints, int movementPoints, int defencePoints)
        {

            _attackPoints += attackPoints;
            _movementPoints += movementPoints;
            _defencePoints =+ defencePoints;

            UnlockAbility();

        }

        public override void UnlockAbility()
        {

            if (_attackPoints >= 2 && _attackPoints < 4)
            {
                _activeAbility = _abilityTier1;
                SetActiveAbility();
            }
            else if (_attackPoints >= 4 && _attackPoints < 6)
            {
                _activeAbility = _abilityTier2;
                SetActiveAbility();
            }
            else if (_attackPoints >= 6)
            {
                _activeAbility = _abilityTier3;
                SetActiveAbility();
            }
            SetActiveAbility();
        }

        public override void SetActiveAbility()
        {


            if (_activeAbility!= null && _player != null && _activeAbility.IsPassive == false)
            {
                _player.GetComponent<AbilityHolder>().ChangeAbility(_activeAbility);
            }
            else if (_activeAbility != null && _player != null && _activeAbility.IsPassive == true)
            {
                _player.GetComponent<AbilityHolder>().ActivatePassiveAbility(_activeAbility);
            }
        }


    }
}
