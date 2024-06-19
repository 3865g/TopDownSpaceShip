using Scripts.Data;
using Scripts.Services.PersistentProgress;
using Scripts.Logic;
using System;
using UnityEngine;

namespace Scripts.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {

        public event Action HealthChanged;
        private State _state;

        
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
            if(!gameObject)
            {
                progress.HeroState.CurrentHP = CurrentHP;
                progress.HeroState.MaxHP = MaxHP;
            }
            

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