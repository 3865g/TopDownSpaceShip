using Scripts.Infrastructure.Factory;
using Scripts.Services.StaticData;
using UnityEngine;
using Scripts.Hero.Ability.ConfigurationStattes;
using Scripts.Services.PersistentProgress;
using Scripts.Data;
using System;

namespace Scripts.Hero.Ability
{


    public class AbilityManager : MonoBehaviour, ISavedProgress
    {
        public ConfigurationAbility activeAbility;


        public GameObject _player;

        private IGameFactory _gameFactory;
        private IStaticDataService _staticDataService;

        private int _attackPoints;
        private int _movementPoints;
        private int _protectivePoints;

        public AbilityData _abilityData { get; private set; }

        private ConfigurationState _currentConfiguration;

        private SkillType _skillType;

        public int _skillTypeId;





        public void Construct(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;

            _staticDataService = staticDataService;
        }


        public void InitPlayer(GameObject player, int skilltypeId)
        {
            _player = player;
            //Need Refactoring?
            _skillTypeId = skilltypeId;
            ChangeConfiguration(_skillTypeId);
        }


        public void ChangeConfiguration(int skillTypeId)
        {
            _skillTypeId = skillTypeId;
            _skillType = (SkillType)Enum.ToObject(typeof(SkillType), _skillTypeId);
           


            switch (_skillType)
            {
                case SkillType.Attacking:
                    {
                        _currentConfiguration = new AttackConfiguration();
                        InitializeConfiguration();
                    }
                    break;
                case SkillType.Movement:
                    {
                        _currentConfiguration = new MovementConfiguration();
                        InitializeConfiguration();
                    }
                    break;
                case SkillType.Protective:
                    {
                        _currentConfiguration = new ProtectiveConfiguration();
                        InitializeConfiguration();
                    }
                    break;
            }

        }


        public void InitializeConfiguration()
        {

            _currentConfiguration.Construct(_staticDataService, _player);
            _currentConfiguration.InitActiveAbility();
            _currentConfiguration.ChangePoints(_attackPoints, _movementPoints, _protectivePoints);
        }

        public void ActivateAbility()
        {

            _currentConfiguration.InitActiveAbility();
        }

        public void CalculatePoints(SecondaryAbility ability)
        {
            _attackPoints = 0;
            _movementPoints = 0;
            _protectivePoints = 0;
            switch (ability.skillType)
            {
                case SkillType.Attacking:
                    {
                        _attackPoints +=  1;
                        _currentConfiguration.ChangePoints(_attackPoints, _movementPoints , _protectivePoints);
                    }
                    break;
                case SkillType.Movement:
                    {
                        _movementPoints += 1;
                        _currentConfiguration.ChangePoints(_attackPoints, _movementPoints, _protectivePoints);
                    }
                    break;
                case SkillType.Protective:
                    {
                        _protectivePoints += 1;
                        _currentConfiguration.ChangePoints(_attackPoints, _movementPoints, _protectivePoints);
                    }
                    break;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.AbilityProgress.SkillTypeId = _skillTypeId;
            progress.AbilityProgress.skillType = _skillType;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress != null)
            {
                _abilityData = progress.AbilityProgress;
                _skillType = progress.AbilityProgress.skillType;
                _skillTypeId = progress.AbilityProgress.SkillTypeId;
            }
            
        }
    }
}
