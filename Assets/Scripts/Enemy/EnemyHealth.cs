using Scripts.Logic;
using System;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float currentHP;
        [SerializeField] private float maxHP;

        public event Action HealthChanged;
        public float CurrentHP 
        { 
            get => currentHP; 
            set => currentHP = value; 
        }
        public float MaxHP 
        { 
            get => maxHP;
            set => maxHP = value; 
        }

        public bool ReturnDamage { get; set; }
        public float ReturnedDamage { get; set; }


        public void TakeDamage(float damage)
        {
            CurrentHP -= damage;

            HealthChanged?.Invoke();
        }

        public void TakeHP(float HP)
        {
            HealthChanged?.Invoke();
        }
    }
}