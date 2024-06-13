using Scripts.Infrastructure.Factory;
using Scripts.Services.StaticData;
using Scripts.StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Hero.Ability.ConfigurationStattes;
using UnityEditor.Playables;

namespace Scripts.Hero.Ability
{


    public class AbilityManager : MonoBehaviour
    {
        //public AbilityTypeId abilityTypeId;
        public Ability activeAbility;

        public SkillType _skillType;


        private IGameFactory _gameFactory;
        public GameObject _player;
        private IStaticDataService _staticDataService;

        private int _attackPoints;
        private int _movementPoints;
        private int _defencePoints;

        private ConfigurationState _currentConfiguration;




        public void Construct(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;

            _staticDataService = staticDataService;
        }

        public void InitPlayer(GameObject player)
        {
            _player = player;
            ChangeConfiguration(_skillType);
        }


        public void ChangeConfiguration(SkillType skillType)
        {
            _skillType = skillType;

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

    }
}
