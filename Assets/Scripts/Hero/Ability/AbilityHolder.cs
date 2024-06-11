using Scripts.Services.Input;
using Scripts.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Hero.Ability
{


    public class AbilityHolder : MonoBehaviour
    {
        public Ability activeAbility;
        public IInputService _inputService;

        public float cooldownTime;
        public float activeTime;

        enum AbilityState
        {
            ready,
            active,
            cooldown
        }

        AbilityState state = AbilityState.ready;




        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            switch (state)
            {
                case AbilityState.ready:
                    if (Input.GetKey(KeyCode.LeftShift))
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
    }
}
