using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using System;
using UnityEngine;

namespace Scripts.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {

        private State _state;
        public event Action HealthChanged;
        public float CurrentHP 

        { 
            get => _state.CurrentHP;
            set
            {
                if(_state.CurrentHP != value)
                {

                    _state.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }

        }
        public float MaxHP 
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
           _state = progress.HeroState;

            HealthChanged?.Invoke();

        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHP = CurrentHP;
            progress.HeroState.MaxHP = MaxHP;

            //Debug.Log(CurrentHP);
        }

        public void TakeDamage(float damage)
        {
            if(CurrentHP <= 0)
            {
                return;
            }
            CurrentHP -= damage;
            //Animator.PlayHit();
        }
    }
}