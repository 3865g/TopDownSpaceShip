using Scripts.Hero;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;

        private HeroHealth _heroHealth;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if(health != null)
            {
                Construct(health);
            }
        }


        public void Construct(HeroHealth health)
        {
            _heroHealth = health;

            _heroHealth.HealthChanged += UpdateHpBar;
        }
        private void OnDestroy()
        {
            _heroHealth.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            HpBar.SetValue(_heroHealth.CurrentHP, _heroHealth.MaxHP);
        }

        

    }
}