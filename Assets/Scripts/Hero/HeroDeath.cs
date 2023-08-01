using Scripts.Enemy;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Hero
{
    [RequireComponent(typeof(HeroDeath))]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth heroHealth;
        public ShipMove shipMove;

        public GameObject DeathFx;

        private HeroAttack _heroAttack;
        private bool _isDead =false;
        private Rigidbody _rigidbody;


        private void Awake()
        {
            _heroAttack = GetComponent<HeroAttack>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            heroHealth.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            heroHealth.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if(!_isDead && heroHealth.CurrentHP <= 0)
            {
                Die();
            }
            //Debug.Log(heroHealth.CurrentHP);
        }

        private void Die()
        {
            _isDead = true;
            shipMove.enabled = false;
            _heroAttack.enabled = false;
            //Animator.Play(Die);
            Destroy(gameObject);
            Instantiate(DeathFx, transform.position, Quaternion.identity);

            //Debug.Log(heroHealth.CurrentHP);
        }
    }
}