using System;

namespace Scripts.Logic
{
    public interface IHealth
    {
        float CurrentHP { get; set; }
        float MaxHP { get; set; }

        event Action HealthChanged;

        void TakeDamage(float damage);
    }
}