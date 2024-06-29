using System;

namespace Scripts.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        float CurrentHP { get; set; }
        float MaxHP { get; set; }

        float ReturnedDamage { get; set; }  

        bool ReturnDamage { get; set; }

        void TakeDamage(float damage);
    }
}