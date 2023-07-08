using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Scripts.Enemy
{
    public class Attack : MonoBehaviour
    {
        public float AttackCooldown = 3f;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask = 1 << LayerMask.NameToLayer("Player");
        private float Clevage = 0.5f;
        private Collider[] _hits = new Collider[1];
        private float _effectiveDistane;
        private bool _attackIsActive;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;
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
            }
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
            //_isAttacking = true;

            transform.LookAt(_heroTransform);
            _attackCooldown = AttackCooldown;

            //_isAttacking = true;
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
            return /* _isAttacking && */_attackIsActive && CooldownIsUp();
        }

        private bool CooldownIsUp()
        {
            return AttackCooldown >= 0;
        }


        private bool Hit(out Collider hit)
        {
            Vector3 startPoint = GetStartPoint();
            int hitsCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), Clevage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitsCount > 0;

        }


    }
}