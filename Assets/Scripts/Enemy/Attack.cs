using Scripts.Hero;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using Scripts.Logic;
using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Scripts.Enemy
{
    public class Attack : MonoBehaviour
    {
        public float AttackCooldown = 1.5f;
        public LayerMask LayerMask;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask; 
        private float Clevage = 10f;
        private Collider[] _hits = new Collider[1];
        private float _effectiveDistane = 1f;
        private bool _attackIsActive;

        [SerializeField]
        private float Damage = 5f;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;
            _layerMask = LayerMask;
        }

        private void Update()
        {

            UpdateCooldown();


            if (CanAttack())
            {
                StartAttack();
            }
            
        }

      

        private void OnAttack()
        {
            if(Hit(out Collider hit))
            {
                //PhysicsDebug.DrawDebug(GetStartPoint(), Clevage, 1);
                
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
                _attackCooldown = AttackCooldown;
            }
            //Debug.Log("Attack");
        }

        private Vector3 GetStartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward * _effectiveDistane;
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
            transform.LookAt(_heroTransform);
            OnAttack();
        }

        private void OnHeroCreated()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }

        internal void EnableAttack()
        {
            _attackIsActive = true;
        }

        internal void DisableAttack()
        {
            _attackIsActive = false;
        }

        private bool CanAttack()
        {
            return  /*_isAttacking &&*/ _attackIsActive && CooldownIsUp();
        }

        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0f;
        }


        private bool Hit(out Collider hit)
        {
            Vector3 startPoint = GetStartPoint();
            int hitsCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), Clevage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            //Debug.Log(hit);
            return hitsCount > 0;

        }


    }
}