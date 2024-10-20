using Scripts.Services.StaticData;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Hero.Ability.ConfigurationStattes
{


    public class DefenceConfiguration : ConfigurationState
    {

        public AbilityTypeId abilityTypeId;
        
        private Ability _activeAbility;
        private Ability _abilityTier1;
        private Ability _abilityTier2;
        private Ability _abilityTier3;

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

            _abilityTier1 = _staticDataService.ForAbility(AbilityTypeId.ShieldTier1);
            _abilityTier2 = _staticDataService.ForAbility(AbilityTypeId.ShieldTier2);
            _abilityTier3 = _staticDataService.ForAbility(AbilityTypeId.ShieldTier3);


            UnlockAbility();
        }

        public override void ChangePoints(int attackPoints, int movementPoints, int defencePoints)
        {
            base.ChangePoints(attackPoints, movementPoints, defencePoints);

            _attackPoints = _attackPoints + attackPoints;
            _movementPoints = _movementPoints + movementPoints;
            _defencePoints = _defencePoints + defencePoints;

            UnlockAbility();

        }

        public override void UnlockAbility()
        {
            base.UnlockAbility();

            if(_defencePoints < 3)
            {
                _activeAbility = _abilityTier1;
                SetActiveAbility();
            }
            else if (_defencePoints >= 3 && _attackPoints < 9)
            {
                _activeAbility = _abilityTier2;
                SetActiveAbility();
            }
            else
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
                _player.GetComponent<AbilityHolder>().ChangeAbility(_activeAbility);
            }
        }
    }
}
