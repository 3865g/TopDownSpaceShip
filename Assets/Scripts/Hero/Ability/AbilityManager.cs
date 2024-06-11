using Scripts.Infrastructure.Factory;
using Scripts.Services.StaticData;
using Scripts.StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Hero.Ability
{


    public class AbilityManager : MonoBehaviour
    {
        public AbilityTypeId abilityTypeId;
        public Ability activeAbility;

        public SkillType _skillType;
        private IGameFactory _gameFactory;
        private GameObject _player;
        private IStaticDataService _staticDataService;




        public void Construct(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;

            _staticDataService = staticDataService;
        }

        public void InitPlayer(GameObject player)
        {
            _player = player;
            SetActiveAbility();
        }


        public void ChangeConfiguration(SkillType skillType)
        {
            _skillType = skillType;

            switch (_skillType)
            {
                case SkillType.Attack:
                    {
                        activeAbility = _staticDataService.ForAbility(AbilityTypeId.DoubleShootTier1);
                    }
                    break;
                case SkillType.Movement:
                    {
                        activeAbility = _staticDataService.ForAbility(AbilityTypeId.DashTier1);
                    }
                    break;
                case SkillType.Defence:
                    {
                        activeAbility = _staticDataService.ForAbility(AbilityTypeId.ShieldTier1);
                    }
                    break;
            }

        }


        public void SetActiveAbility()
        {

            

            if (_player != null)
            {
                _player.GetComponent<AbilityHolder>().activeAbility = activeAbility;
                //Debug.LogError(_player.GetComponent<AbilityHolder>().activeAbility);
            }
        }
    }
}
