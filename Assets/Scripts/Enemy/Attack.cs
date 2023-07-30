using Scripts.Infrastructure.AssetManagement;
using Scripts.Logic;
using System.Linq;
using UnityEngine;

namespace Scripts.Enemy
{
    public class Attack : MonoBehaviour
    {
        public float AttackCooldown = 1.5f;
        public LayerMask LayerMask;
        public float Damage = 10f;
        public float Cleavage = 10f;
        public float EffectiveDistane = 10f;

        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask; 
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void Awake()
        {
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
                if (hit.CompareTag("Player"))
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
                _attackCooldown = AttackCooldown;
                //Debug.Log("AttackHero");
            }
        }

        private Vector3 GetStartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward * EffectiveDistane;
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
            int hitsCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), Cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            //Debug.Log(hit);
            return hitsCount > 0;

        }

        
    }
}