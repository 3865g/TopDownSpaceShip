using Scripts.Infrastructure.Factory;
using Scripts.Services.StaticData;
using Scripts.StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Hero.Ability.ConfigurationStattes;
using Scripts.Services.PersistentProgress;
using Scripts.Data;
using Unity.VisualScripting;
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

        public string test;

        private ConfigurationState _currentConfiguration;

        private SkillType _skillType;

        private ISaveLoadService _saveLoadService;

        public int _skillTypeId;
        //{
        //    get; private set;

        //}





        public void Construct(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;

            _staticDataService = staticDataService;
        }

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
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
            test = _skillType.ToString();
            _saveLoadService.SaveProgress();


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
            _currentConfiguration.InitActiveAbility();
        }



        public void AddedAbility(Ability ability)
        {
           CalculatePoints(ability);

        }

        public void CalculatePoints(Ability ability)
        {
            switch (ability.skillType)
            {
                case SkillType.Attack:
                    {
                        _attackPoints = _attackPoints + ability.Point;
                        _currentConfiguration.ChangePoints(_attackPoints, _movementPoints , _defencePoints);
                    }
                    break;
                case SkillType.Movement:
                    {
                        _movementPoints = _movementPoints + ability.Point;
                        _currentConfiguration.ChangePoints(_attackPoints, _movementPoints, _defencePoints);
                    }
                    break;
                case SkillType.Defence:
                    {
                        _defencePoints = _defencePoints + ability.Point;
                        _currentConfiguration.ChangePoints(_attackPoints, _movementPoints, _defencePoints);
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
            _abilityData = progress.AbilityProgress;
            _skillType = progress.AbilityProgress.skillType;
            _skillTypeId = progress.AbilityProgress.SkillTypeId;
            ChangeConfiguration(_skillTypeId);
        }
    }
}
