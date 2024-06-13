using Scripts.Hero.Ability.ConfigurationStattes;
using Scripts.Services.StaticData;
using Scripts.StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Hero.Ability.ConfigurationStattes
{


    public class AttackConfiguration : ConfigurationState
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
            base.Construct(staticDataService, player);

            _staticDataService = staticDataService;
            _player = player;
        }

        public override void InitActiveAbility()
        {
            base.InitActiveAbility();

            _abilityTier1 = _staticDataService.ForAbility(AbilityTypeId.DoubleShootTier1);

            UnlockAbility();
        }

        public override void ChangePoints(int attackPoints, int movementPoints, int defencePoints)
        {
            base.ChangePoints(attackPoints, movementPoints, defencePoints);

            _attackPoints = attackPoints;
            _movementPoints = movementPoints;
            _defencePoints = defencePoints;

            UnlockAbility();

        }

        public override void UnlockAbility()
        {
            base.UnlockAbility();

            if (_attackPoints < 3)
            {
                _activeAbility = _abilityTier1;
                SetActiveAbility();
            }
            else if (_attackPoints >= 3 && _attackPoints < 9)
            {
                _activeAbility = _abilityTier2;
                SetActiveAbility();
            }
            else
            {
                _activeAbility = _abilityTier3;
                SetActiveAbility();
            }
            SetActiveAbility();
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
