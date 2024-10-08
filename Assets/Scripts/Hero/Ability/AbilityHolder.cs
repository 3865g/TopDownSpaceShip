using Scripts.Services.Input;
using Scripts.Services;
using System.Collections.Generic;

using UnityEngine;
using Scripts.Logic;
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

        public int CurrentAbilityState;

        public bool IsAbilityUse;

        public Ability activeAbility;
        


        //public List<Ability> passiveAbilities;
        public List<SecondaryAbility> secondaryAbilities;
        public RewardsManager RewardsManager;

        private Ability _passiveAbility;
        private SecondaryAbility _secondaryAbility;
        private AbilityManager _abilityManager;

        private float _pausedCooldownTime;
        private float _pausedActiveTime;
        private bool _saveAfterPause = false;
        private bool _loadAfterPause = false;
        private AbilityState _pauseState;

        //Need Refactoring
        private bool _isAbilityUseKeyboard;

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

            //Need refactoring (custom pause?)

            if(Time.timeScale == 0 && !_saveAfterPause)
            {
                SavePause();
                return;
            }
            else if (Time.timeScale != 0 && !_loadAfterPause)
            {
                LoadAfterPause();
            }


            if (Input.GetKey(KeyCode.LeftShift))
            {
                _isAbilityUseKeyboard = true;
            }
            else
            {
                _isAbilityUseKeyboard = false;
            }


            switch (state)
            {
                case AbilityState.ready:
                    if ((IsAbilityUse || _isAbilityUseKeyboard) && activeAbility)
                    {
                        activeAbility.Activate(gameObject);
                        state = AbilityState.active;
                        activeTime = activeAbility.ActiveTime;
                        cooldownTime = activeAbility.ColdownTime * cooldownMultiplayer;
                        CurrentAbilityState = 0;
                        IsAbilityUse = false;
                    }
                    break;
                case AbilityState.active:
                    if (activeTime > 0)
                    {
                        activeTime -= Time.deltaTime;
                        CurrentAbilityState = 1;
                        IsAbilityUse = false;
                    }
                    else
                    {
                        state = AbilityState.cooldown;
                        activeAbility.Deactivate(gameObject);
                        cooldownTime = activeAbility.ColdownTime * cooldownMultiplayer;
                        IsAbilityUse = false;
                    }
                    break;
                case AbilityState.cooldown:
                    if (cooldownTime > 0)
                    {
                        cooldownTime -= Time.deltaTime;
                        CurrentAbilityState = 2;
                        IsAbilityUse = false;
                    }
                    else
                    {
                        state = AbilityState.ready;
                    }
                    break;
            }

        }


        public void SavePause()
        {
            _pauseState = state;
            _pausedCooldownTime = cooldownTime;
            _pausedActiveTime = activeTime;
            _saveAfterPause = true;
            _loadAfterPause = false;
        }

        public void LoadAfterPause()
        {
            state = _pauseState;
            cooldownTime = _pausedCooldownTime;
            activeTime = _pausedActiveTime;
            _saveAfterPause = false;
            _loadAfterPause = true;
        }


        public void ActivatePassiveAbility(Ability passiveAbility)
        {
            _passiveAbility = passiveAbility;
            _passiveAbility.ActivatePassive(gameObject);
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
