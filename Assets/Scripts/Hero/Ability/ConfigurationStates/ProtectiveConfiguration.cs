using Scripts.Services.StaticData;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Hero.Ability.ConfigurationStattes
{


    public class ProtectiveConfiguration : ConfigurationState
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
        private int _protectivePoints;

        private AbilityHolder _abilityHolder;



        public override void Construct(IStaticDataService staticDataService, GameObject player)
        {
            _staticDataService =staticDataService;
            _player = player;
        }

        public override void InitActiveAbility()
        {

            _abilityTier1 = _staticDataService.ForAbility(AbilityTypeId.ShieldTier1);
            _abilityTier2 = _staticDataService.ForAbility(AbilityTypeId.ShieldTier2);
            _abilityTier3 = _staticDataService.ForAbility(AbilityTypeId.ShieldTier3);

            if (_player != null)
            {
                AbilityHolder abilityHolder = _player.GetComponent<AbilityHolder>();

                abilityHolder.ConfigurationDescription = _staticDataService.ForConfiguration(ConfigurationTypeId.ProtectiveСonfiguration);
            }


            UnlockAbility();
        }

        public override void ChangePoints(int attackPoints, int movementPoints, int defencePoints)
        {

            _attackPoints = _attackPoints + attackPoints;
            _movementPoints = _movementPoints + movementPoints;
            _protectivePoints = _protectivePoints + defencePoints;


            if (_player != null) 
            {
                AbilityHolder abilityHolder = _player.GetComponent<AbilityHolder>();
                abilityHolder.CurentPoints = _protectivePoints;
            }

            

            UnlockAbility();

        }

        public override void UnlockAbility()
        {

            if(_protectivePoints >= 2 && _protectivePoints < 4)
            {
                _activeAbility = _abilityTier1;
                SetActiveAbility();
            }
            else if (_protectivePoints >= 4 && _protectivePoints < 6)
            {
                _activeAbility = _abilityTier2;
                SetActiveAbility();
            }
            else if (_protectivePoints >= 6)
            {
                _activeAbility = _abilityTier3;
                SetActiveAbility();
            }
        }

        public override void SetActiveAbility()
        {
            base.SetActiveAbility();

            if (_player != null)
            {
                AbilityHolder abilityHolder =  _player.GetComponent<AbilityHolder>();
                abilityHolder.ChangeAbility(_activeAbility);
                abilityHolder.ConfigurationDescription = _staticDataService.ForConfiguration(ConfigurationTypeId.ProtectiveСonfiguration);
            }
        }
    }
}
