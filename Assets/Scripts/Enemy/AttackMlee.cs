using Scripts.Hero.Ability;
using Scripts.Logic;
using System.Linq;
using UnityEngine;

namespace Scripts.Enemy
{
    public class AttackMlee : MonoBehaviour, IAttack
    {

        public float AttackCooldown = 1.5f;
        public float Damage = 10f;
        public float Cleavage = 10f;
        public float EffectiveDistane = 10f;
        public float Radius;
        public bool Kamikaze;
        public LayerMask LayerMask;

        private EnemyDeath _enemyDeath;
        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private bool _attackIsActive;
        private float _radius;
        private Collider[] _hits = new Collider[3];
        private int _layerMask;

        public void Construct(Transform heroTransform, float damage)
        {
            _heroTransform = heroTransform;
            Damage = damage;
        }


        private void Awake()
        {
            _radius = Radius;
            _layerMask = LayerMask;
            _enemyDeath = gameObject.GetComponent<EnemyDeath>();
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
                _attackCooldown = AttackCooldown;
                switch (hit.tag)
                {
                    case "Player":
                        hit.transform.GetComponent<IHealth>().TakeDamage(Damage, Color.gray);
                        CheckSelfDestroy();
                        break;
                    case "PlayerShield":
                        hit.transform.GetComponent<ShieldPrefab>().AbilityHolder.DeactivateAbility();
                        CheckSelfDestroy();
                        break;
                }              

                
            }

        }

        private bool Hit(out Collider hit)
        {
            var hitAmount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _hits, _layerMask);
            hit = _hits.FirstOrDefault();

            return hitAmount > 0;
        }

        public void CheckSelfDestroy()
        {
            switch (Kamikaze)
            {
                case false:
                    break;
                case true:
                    _enemyDeath.Die();
                    break;
            }
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
            return  /*_isAttacking &&*/ _attackIsActive && CooldownIsUp() && _heroTransform != null;
        }

        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0f;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }


    }
}