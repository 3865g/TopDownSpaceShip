using Scripts.Services.Input;
using Scripts.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Scripts.Hero.Ability
{


    public class AbilityHolder : MonoBehaviour
    {
        public IInputService _inputService;

        public float cooldownTime;
        public float activeTime;

        public Ability activeAbility;
        private Ability _passiveAbility;

        public List<Ability> passiveAbilities;

        enum AbilityState
        {
            ready,
            active,
            cooldown
        }

        AbilityState state = AbilityState.ready;


        //Need Refactoring

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
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
                        cooldownTime = activeAbility.ColdownTime;
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
                        cooldownTime = activeAbility.ColdownTime;
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


        public void ChangeAbility(Ability ability)
        {
            activeAbility = ability;
            state = AbilityState.ready;
        }
    }
}
