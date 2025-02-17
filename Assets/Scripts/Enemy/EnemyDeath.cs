﻿using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;
        public GameObject DeathFx;

        public event Action Happened;

        private void Start()
        {
            Health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            Health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if(Health.CurrentHP <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            //Animator.PlayDeath();
            Health.HealthChanged -= HealthChanged;

            SpawnDeathFx();
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private void SpawnDeathFx()
        {
            GameObject explosion = Instantiate(DeathFx, transform.position, Quaternion.identity);

            Destroy(explosion, 2f);
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(.1f);
            Destroy(gameObject);
        }
    }
}