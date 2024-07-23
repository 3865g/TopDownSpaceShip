using Scripts.Infrastructure.Factory;
using Scripts.Services.StaticData;
using UnityEngine;
using Scripts.Hero.Ability.ConfigurationStattes;
using Scripts.Services.PersistentProgress;
using Scripts.Data;
using System;
using Scripts.Services.SaveLoad;
using Scripts.Services;

namespace Scripts.Hero.Ability
{


    public class AbilityManager : MonoBehaviour, ISavedProgress
    {
        //public AbilityTypeId abilityTypeId;
        public Ability activeAbility;


        public GameObject _player;

        private IGameFactory _gameFactory;
        private IStaticDataService _staticDataService;

        private int _attackPoints;
        private int _movementPoints;
        private int _defencePoints;

        public AbilityData _abilityData { get; private set; }

        //public string test;

        private ConfigurationState _currentConfiguration;

        private SkillType _skillType;

        public int _skillTypeId;
        //{
        //    get; private set;

        //}





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
                case SkillType.Attack:
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
                case SkillType.Defence:
                    {
                        _currentConfiguration = new DefenceConfiguration();
                        InitializeConfiguration();
                    }
                    break;
            }

        }


        public void InitializeConfiguration()
        {

            _currentConfiguration.Construct(_staticDataService, _player);
            _currentConfiguration.ChangePoints(_attackPoints, _movementPoints, _defencePoints);
            _currentConfiguration.InitActiveAbility();
        }



        public void AddedAbility(Ability ability)
        {
          // CalculatePoints(ability);

        }

        public void CalculatePoints(SecondaryAbility ability)
        {
            switch (ability.skillType)
            {
                case SkillType.Attack:
                    {
                        _attackPoints +=  ability.Point;
                        _currentConfiguration.ChangePoints(_attackPoints, _movementPoints , _defencePoints);
                    }
                    break;
                case SkillType.Movement:
                    {
                        _movementPoints += ability.Point;
                        _currentConfiguration.ChangePoints(_attackPoints, _movementPoints, _defencePoints);
                    }
                    break;
                case SkillType.Defence:
                    {
                        _defencePoints += ability.Point;
                        _currentConfiguration.ChangePoints(_attackPoints, _movementPoints, _defencePoints);
                    }
                    break;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.AbilityProgress.SkillTypeId = _skillTypeId;
            progress.AbilityProgress.skillType = _skillType;
            //progress.AbilityProgress.AttackPoints = _attackPoints;
            //progress.AbilityProgress.MovementPoints = _movementPoints;
            //progress.AbilityProgress.DefencePoints = _defencePoints;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _abilityData = progress.AbilityProgress;
            _skillType = progress.AbilityProgress.skillType;
            _skillTypeId = progress.AbilityProgress.SkillTypeId;
            //_attackPoints = progress.AbilityProgress.AttackPoints;
            //_movementPoints = progress.AbilityProgress.MovementPoints;
            //_defencePoints = progress.AbilityProgress.DefencePoints;

            //Debug.LogError(progress.AbilityProgress); 

            //InitializeConfiguration();
            //ChangeConfiguration(_skillTypeId);
        }
    }
}
