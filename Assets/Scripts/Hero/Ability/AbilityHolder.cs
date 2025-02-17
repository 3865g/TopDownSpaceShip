using Scripts.Services.Input;
using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Logic;
using Scripts.Data;
using Scripts.Services.PersistentProgress;
using Scripts.UI.Elements;


namespace Scripts.Hero.Ability
{
    public class AbilityHolder : MonoBehaviour, ISavedProgress
    {
        public float cooldownTime;
        public float cooldownMultiplayer;
        public float activeTime;

        public int CurrentAbilityState;

        public bool IsAbilityUse;

        public ConfigurationAbility activeAbility;

        public ConfigurationDescription ConfigurationDescription;
        public int CurentPoints;


        //public List<Ability> passiveAbilities;
        public List<SecondaryAbility> SecondaryAbilities;
        public List<SecondaryAbility> AttributeAbilities;
        public RewardsManager RewardsManager;

        private ConfigurationAbility _passiveAbility;
        private SecondaryAbility _secondaryAbility;
        private SecondaryAbility _attributeAbility;
        private AbilityManager _abilityManager;

        private float _pausedCooldownTime;
        private float _pausedActiveTime;
        private int _pausedIndexState;
        private bool _saveAfterPause = false;
        private bool _loadAfterPause = false;
        private AbilityState _pauseState;
        private AbilityUI _abilityUi;
        private IInputService _inputService;

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

        public void InitHUD(AbilityUI abilityUI)
        {
            _abilityUi = abilityUI;
            _abilityUi.AbilityButton.gameObject.SetActive(false);
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

            if (Time.timeScale == 0 && !_saveAfterPause)
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
                    CurrentAbilityState = 0;
                    if ((IsAbilityUse || _isAbilityUseKeyboard) && activeAbility)
                    {
                        activeAbility.Activate(gameObject);
                        state = AbilityState.active;
                        activeTime = activeAbility.ActiveTime;
                        cooldownTime = activeAbility.ColdownTime * cooldownMultiplayer;
                        IsAbilityUse = false;
                    }

                    break;
                case AbilityState.active:
                    if (activeTime > 0)
                    {
                        activeTime -= Time.deltaTime;
                        _abilityUi.AbilityButton.ButtonActive(activeTime);
                        CurrentAbilityState = 1;
                        IsAbilityUse = false;
                    }
                    else
                    {
                        state = AbilityState.cooldown;
                        activeAbility.Deactivate(gameObject);
                        cooldownTime = activeAbility.ColdownTime * cooldownMultiplayer;
                        _abilityUi.AbilityButton.ButtoonCooldown(cooldownTime, activeAbility.ColdownTime);
                        IsAbilityUse = false;
                    }

                    break;
                case AbilityState.cooldown:
                    if (cooldownTime > 0)
                    {
                        cooldownTime -= Time.deltaTime;
                        _abilityUi.AbilityButton.ButtoonCooldown(cooldownTime, activeAbility.ColdownTime);
                        CurrentAbilityState = 2;
                        IsAbilityUse = false;
                    }
                    else
                    {
                        state = AbilityState.ready;
                        _abilityUi.AbilityButton.ButtonReady();
                        CurrentAbilityState = 0;
                    }

                    break;
            }
        }


        public void SavePause()
        {
            _pauseState = state;
            _pausedCooldownTime = cooldownTime;
            //_pausedIndexState = CurrentAbilityState;
            _pausedActiveTime = activeTime;
            _saveAfterPause = true;
            _loadAfterPause = false;
        }

        public void LoadAfterPause()
        {
            state = _pauseState;
            //CurrentAbilityState = _pausedIndexState;
            cooldownTime = _pausedCooldownTime;
            activeTime = _pausedActiveTime;

            if (activeAbility)
            {
                _abilityUi.AbilityButton.ButtonActive(activeTime);
                _abilityUi.AbilityButton.ButtoonCooldown(cooldownTime, activeAbility.ColdownTime);
            }

            _saveAfterPause = false;
            _loadAfterPause = true;
        }


        public void ActivatePassiveAbility(ConfigurationAbility passiveAbility)
        {
            _passiveAbility = passiveAbility;
            _abilityUi.AbilityButton.gameObject.SetActive(true);
            _abilityUi.Slider.SetActive(false);
            _abilityUi.AbilityButton.AbilityImage.sprite = _passiveAbility.Icon;
            _passiveAbility.ActivatePassive(gameObject);
        }

        public void ActivateSecondaryAbility(SecondaryAbility secondaryAbility)
        {
            if (!SecondaryAbilities.Contains(secondaryAbility))
            {
                _secondaryAbility = secondaryAbility;
                _secondaryAbility.ActivatePassive(gameObject);
                SecondaryAbilities.Add(_secondaryAbility);
                RewardsManager.UpdateList(_secondaryAbility);

                _abilityManager.CalculatePoints(_secondaryAbility);
            }
        }

        public void ActivateStatAbility(SecondaryAbility secondaryAbility)
        {
            _attributeAbility = secondaryAbility;
            _attributeAbility.ActivatePassive(gameObject);
            AttributeAbilities.Add(_attributeAbility);
        }


        public void ChangeAbility(ConfigurationAbility ability)
        {
            if (CurrentAbilityState != 0 && activeAbility != ability)
            {
                activeAbility.Deactivate(gameObject);
                activeTime = 0;
                cooldownTime = 0;
                _pausedCooldownTime = 0;
                _pausedActiveTime = 0;
                _pauseState = 0;
                CurrentAbilityState = 0;
            }

            activeAbility = ability;
            _abilityUi.AbilityButton.gameObject.SetActive(true);
            _abilityUi.AbilityButton.AbilityImage.sprite = ability.Icon;
            state = AbilityState.ready;
        }


        public void ActivateSecondaryAbilitiesAfterLoad()
        {
            foreach (SecondaryAbility element in SecondaryAbilities)
            {
                _secondaryAbility = element;
                RewardsManager.UpdateList(_secondaryAbility);
                _abilityManager.CalculatePoints(_secondaryAbility);

                if (_secondaryAbility.ReActivateAfterLoad)
                {
                    _secondaryAbility.ActivatePassive(gameObject);
                }
            }
        }

        public void ActivateAttributeAbilitiesAfterLoad()
        {
            foreach (SecondaryAbility element in AttributeAbilities)
            {
                _attributeAbility = element;

                if (_attributeAbility.ReActivateAfterLoad)
                {
                    _attributeAbility.ActivatePassive(gameObject);
                }
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.AbilityProgress.secondaryAbilitiesData = SecondaryAbilities;
            progress.AbilityProgress.attributeAbilitiesData = AttributeAbilities;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LoadSecondaryAbilities(progress);

            LoadAttributeAbilities(progress);

            RefreshAbility();
        }

        public void LoadSecondaryAbilities(PlayerProgress progress)
        {
            if (progress.AbilityProgress.secondaryAbilitiesData != null)
            {
                SecondaryAbilities = progress.AbilityProgress.secondaryAbilitiesData;
            }
            else
            {
                SecondaryAbilities = new List<SecondaryAbility>();
            }

            if (SecondaryAbilities.Count > 0)
            {
                ActivateSecondaryAbilitiesAfterLoad();
            }
        }

        public void LoadAttributeAbilities(PlayerProgress progress)
        {
            if (progress.AbilityProgress.attributeAbilitiesData != null)
            {
                AttributeAbilities = progress.AbilityProgress.attributeAbilitiesData;
            }
            else
            {
                AttributeAbilities = new List<SecondaryAbility>();
            }

            if (AttributeAbilities.Count > 0)
            {
                ActivateAttributeAbilitiesAfterLoad();
            }
        }

        public void DeactivateAbility()
        {
            activeTime = 0;
        }

        private void RefreshAbility()
        {
            activeAbility?.Deactivate(gameObject);
            activeTime = 0;
            cooldownTime = 0;
            _pausedCooldownTime = 0;
            _pausedActiveTime = 0;
            //_pauseState = 0;

            state = AbilityState.ready;

            if (_abilityUi.AbilityButton)
            {
                _abilityUi.AbilityButton.ButtonReady();
            }
        }
    }
}