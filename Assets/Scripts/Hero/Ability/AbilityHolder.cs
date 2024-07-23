using Scripts.Services.Input;
using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Scripts.Logic;
using Scripts.Services.SaveLoad;
using Scripts.Data;
using Scripts.Services.PersistentProgress;


namespace Scripts.Hero.Ability
{


    public class AbilityHolder : MonoBehaviour, ISavedProgress
    {
        public IInputService _inputService;

        public float cooldownTime;
        public float cooldownMultiplayer;
        public float activeTime;

        public Ability activeAbility;

        public List<Ability> passiveAbilities;
        public List<SecondaryAbility> secondaryAbilities;
        public RewardsManager RewardsManager;

        private Ability _passiveAbility;
        private SecondaryAbility _secondaryAbility;
        private AbilityManager _abilityManager; 

        enum AbilityState
        {
            ready,
            active,
            cooldown
        }

        AbilityState state = AbilityState.ready;

        public void Construct(AbilityManager abilityManager)
        {
            _abilityManager = abilityManager;
        }

        //Need Refactoring

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            cooldownMultiplayer = 1;
        }

        private void Update()
        {
            switch (state)
            {
                case AbilityState.ready:
                    if (Input.GetKey(KeyCode.LeftShift) && activeAbility != null)
                    {
                        activeAbility.Activate(gameObject);
                        state = AbilityState.active;
                        activeTime = activeAbility.ActiveTime;
                        cooldownTime = activeAbility.ColdownTime * cooldownMultiplayer;
                    }
                    break;
                case AbilityState.active:
                    if (activeTime > 0)
                    {
                        activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = AbilityState.cooldown;
                        activeAbility.Deactivate(gameObject);
                        cooldownTime = activeAbility.ColdownTime * cooldownMultiplayer;
                    }
                    break;
                case AbilityState.cooldown:
                    if (cooldownTime > 0)
                    {
                        cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = AbilityState.ready;
                    }
                    break;
            }
        }


        public void ActivatePassiveAbility(Ability passiveAbility)
        {
            if (!passiveAbilities.Contains(passiveAbility))
            {
                _passiveAbility = passiveAbility;
                _passiveAbility.ActivatePassive(gameObject);
                passiveAbilities.Add(_passiveAbility);
            }         
        }  
        public void ActivateSecondaryAbility(SecondaryAbility secondaryAbility)
        {
            if (!secondaryAbilities.Contains(secondaryAbility))
            {
                _secondaryAbility = secondaryAbility;
                _secondaryAbility.ActivatePassive(gameObject);
                secondaryAbilities.Add(_secondaryAbility);
                RewardsManager.UpdateList(_secondaryAbility);

                _abilityManager.CalculatePoints(_secondaryAbility);
            }         
        }




        public void ChangeAbility(Ability ability)
        {
            activeAbility = ability;
            state = AbilityState.ready;
        }

      public void ActivateAbilitiesAfterLoad()
        {
            foreach (SecondaryAbility element in secondaryAbilities)
            {
                _secondaryAbility = element;
                _secondaryAbility.ActivatePassive(gameObject);
                RewardsManager.UpdateList(_secondaryAbility);
                _abilityManager.CalculatePoints(_secondaryAbility);
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            //progress.AbilityProgress.secondaryAbility = _secondaryAbility;
             progress.AbilityProgress.secondaryAbilitiesData = secondaryAbilities;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            //activeAbility = progress.AbilityProgress.ability;
            //_secondaryAbility = progress.AbilityProgress.secondaryAbility;
            if (progress.AbilityProgress.secondaryAbilitiesData != null)
            {
                secondaryAbilities = progress.AbilityProgress.secondaryAbilitiesData;
            }
            else
            {
                secondaryAbilities = new List<SecondaryAbility>();
            }

            if (secondaryAbilities.Count > 0)
            {
                ActivateAbilitiesAfterLoad();
            }

        }
    }
}
