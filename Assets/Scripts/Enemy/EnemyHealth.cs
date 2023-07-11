using Scripts.Logic;
using System;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField]
        private float currentHP;
        [SerializeField]
        private float maxHP;

        public float CurrentHP { get => currentHP; set => currentHP = value; }
        public float MaxHP { get => maxHP; set => maxHP = value; }

        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            CurrentHP -= damage;

            HealthChanged?.Invoke();
        }

    }
}