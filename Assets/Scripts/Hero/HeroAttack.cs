using System;
using Scripts.Data;
using Scripts.Services;
using Scripts.Services.PersistentProgress;
using Scripts.Logic;
using Scripts.Services.Input;
using UnityEngine;
using Scripts.Hero;
using Scripts.Infrastructure.AssetManagement;
using Assets.Scripts.Hero;
using UnityEngine.EventSystems;

namespace Scripts.Enemy
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public float AttackCooldown = 0.1f;
        public float LaserSpeed = 500f;
        public bool CanAttack;
        public Transform LaserStartTransform;
        public GameObject Laserprefab;


        private Stats _stats;
        private float _attackCooldown;
        private RotateForAttack _roatateForAttack;

        private void Awake()
        {
            _roatateForAttack = gameObject.GetComponent<RotateForAttack>();
        }

        private void Update()
        {

            UpdateCooldown();


            if (ReadyAttack())
            {
                OnAttack();
            }

            //Debug.Log(_attackCooldown);
        }

        public void OnAttack()
        {
            GameObject laserPrefab = Instantiate(Laserprefab, LaserStartTransform.position, Quaternion.identity);
            Laser laser = laserPrefab.GetComponent<Laser>();
            Vector3 laserDirection = (_roatateForAttack._enemy.transform.position - LaserStartTransform.position).normalized;            
            laser.Construct(laserDirection, _stats.Damage);
            
            _attackCooldown = AttackCooldown;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }

        private bool ReadyAttack()
        {
            return  /*_isAttacking &&*/ CanAttack && CooldownIsUp();
        }



        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _attackCooldown -= Time.deltaTime;
            }

        }
        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0f;
        }


    }
}