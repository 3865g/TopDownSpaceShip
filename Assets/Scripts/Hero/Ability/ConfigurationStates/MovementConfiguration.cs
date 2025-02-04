using Scripts.Services.StaticData;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Hero.Ability.ConfigurationStattes
{


    public class MovementConfiguration : ConfigurationState
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
            _staticDataService =staticDataService;
            _player = player;
        }

        public override void InitActiveAbility()
        {

            _abilityTier1 = _staticDataService.ForAbility(AbilityTypeId.DashTier1);
            _abilityTier2 = _staticDataService.ForAbility(AbilityTypeId.DashTier2);
            _abilityTier3 = _staticDataService.ForAbility(AbilityTypeId.DashTier3);

            if (_player != null)
            {
                AbilityHolder abilityHolder = _player.GetComponent<AbilityHolder>();

                abilityHolder.ConfigurationDescription = _staticDataService.ForConfiguration(ConfigurationTypeId.MovementConfiguration);
            }

            UnlockAbility();
        }

        public override void ChangePoints(int attackPoints, int movementPoints, int defencePoints)
        {

            _attackPoints += attackPoints;
            _movementPoints +=  movementPoints;
            _defencePoints +=  defencePoints;

            if (_player != null)
            {
                AbilityHolder abilityHolder = _player.GetComponent<AbilityHolder>();
                abilityHolder.CurentPoints = _movementPoints;
            }

            UnlockAbility();

        }

        public override void UnlockAbility()
        {

            if(_movementPoints >= 2 && _movementPoints < 4)
            {
                _activeAbility = _abilityTier1;
                SetActiveAbility();
            }
            else if (_movementPoints >= 4 && _movementPoints < 6)
            {
                _activeAbility = _abilityTier2;
                SetActiveAbility();
            }
            else if(_movementPoints >= 6)
            {
                _activeAbility = _abilityTier3;
                SetActiveAbility();
            }
        }

        public override void SetActiveAbility()
        {

            if (_player != null)
            {
                AbilityHolder abilityHolder = _player.GetComponent<AbilityHolder>();
                abilityHolder.ChangeAbility(_activeAbility);
            }
        }

    }
}
