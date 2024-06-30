using Scripts.Data;
using Scripts.Services.PersistentProgress;
using Scripts.Logic;
using System;
using UnityEngine;
using Scripts.Services.Randomizer;

namespace Scripts.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {

        public event Action HealthChanged;

        public bool CanDodge;
        public int DodgeChance;

        private State _state;
        private IRandomService _randomService;
        private int _randomValue;

        
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

        public bool ReturnDamage { get; set; }
        public float ReturnedDamage { get; set; }


        public void Construct(IRandomService randomService)
        {
            _randomService = randomService;
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

            if (CanDodge)
            {
                CalculateRandomValue();

                if (CurrentHP <= 0)
                {
                    return;
                }
                else if (DodgeChance <= _randomValue)
                {
                    //Play Dodge Effect
                    return;
                }
                CurrentHP -= damage;

            }
            else
            {
                if (CurrentHP <= 0)
                {
                    return;
                }
                CurrentHP -= damage;
                //Animator.PlayHit();
            }



        }

        public void TakeHP(float Hp)
        {
            if (CurrentHP <= 0 || CurrentHP >= MaxHP)
            {
                return;
            }
            CurrentHP += Hp;
        }

        public void AddedBonusMaxHP(float bonusHP)
        {
            MaxHP += bonusHP;
            CurrentHP += bonusHP;
        }

        public void UpdateDodgeChance(int dodgeChance)
        {
            DodgeChance += dodgeChance;
        }

        public void CalculateRandomValue()
        {
            _randomValue = _randomService.Next(0,100);
        }
    }
}