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

            if (_player != null)
            {
                AbilityHolder abilityHolder = _player.GetComponent<AbilityHolder>();

                abilityHolder.ConfigurationDescription = _staticDataService.ForConfiguration(ConfigurationTypeId.AttackingConfiguration);
            }

            UnlockAbility();
        }

        public override void ChangePoints(int attackPoints, int movementPoints, int defencePoints)
        {

            _attackPoints += attackPoints;
            _movementPoints += movementPoints;
            _defencePoints =+ defencePoints;


            if (_player != null)
            {
                AbilityHolder abilityHolder = _player.GetComponent<AbilityHolder>();
                abilityHolder.CurentPoints = _attackPoints;
            }
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
                AbilityHolder abilityHolder = _player.GetComponent<AbilityHolder>();
                abilityHolder.ChangeAbility(_activeAbility);
                abilityHolder.ConfigurationDescription = _staticDataService.ForConfiguration(ConfigurationTypeId.Protective—onfiguration);
            }
            else if (_activeAbility != null && _player != null && _activeAbility.IsPassive == true)
            {
                AbilityHolder abilityHolder = _player.GetComponent<AbilityHolder>();
                abilityHolder.ActivatePassiveAbility(_activeAbility);
                abilityHolder.ConfigurationDescription = _staticDataService.ForConfiguration(ConfigurationTypeId.Protective—onfiguration);
                
            }
        }


    }
}
