﻿using Assets.Scripts.Hero;
using Scripts.Hero;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Logic;
using System;
using System.Linq;
using UnityEngine;

namespace Scripts.Enemy
{
    public class AttackFlamethrower : MonoBehaviour, IAttack
    {

        public float AttackCooldown = 1.5f;
        public float Damage = 10f;
        public float Cleavage = 10f;
        public float EffectiveDistane = 10f;
        public Transform[] ShootStartTransforms;
        public GameObject FlamethrowerPrefab;


        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private bool _attackIsActive;
        int i = 0;

        public void Construct(Transform heroTransform, float damage)
        {
            _heroTransform = heroTransform;
            Damage = damage;
        }





        private void Update()
        {
            UpdateCooldown();


            if (CanAttack())
            {
                StartAttack();
            }
            else
            {
                StopAttack();
            }
        }

        private void StopAttack()
        {
            FlamethrowerPrefab.GetComponent<EnemyFlamethrower>().Disable();
        }

        private void OnAttack()
        {

            FlamethrowerPrefab.GetComponent<EnemyFlamethrower>().Enable();
        }

        private Transform ShootStartPosition()
        {

            Transform startTransform = ShootStartTransforms[i];
            i++;

            if (i == ShootStartTransforms.Length)
            {
                i = 0;
            }


            return startTransform;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _attackCooldown -= Time.deltaTime;
            }

        }
        private void StartAttack()
        {
            //transform.LookAt(_heroTransform);
            OnAttack();
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }

        public void DisableAttack()
        {
            _attackIsActive = false;
        }

        private bool CanAttack()
        {
            return  /*_isAttacking &&*/ _attackIsActive && _heroTransform != null;
        }

        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0f;
        }


    }
}