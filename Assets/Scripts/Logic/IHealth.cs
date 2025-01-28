using System;
using UnityEngine;

namespace Scripts.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        float CurrentHP { get; set; }
        float MaxHP { get; set; }

        float ReturnedDamage { get; set; }  

        bool ReturnDamage { get; set; }

        GameObject TextPrefab { get; set; }

        void TakeDamage(float damage, Color color);
        void TakeHP(float HP);

        void AddedBonusMaxHP(float bonusHP);
    }
}