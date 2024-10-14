using Scripts.Logic;
using System;
using UnityEngine;

namespace Scripts.Hero
{
    public class EnemyLaser : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        public float Speed = 150f;
        //public Transform Target;

        private bool _isCollidet;
        private float _damage;
        private GameObject _enemy;


        public void Construct(Vector3 laserDirection, float damage, GameObject parentEmeny)
        {
            _damage = damage;
            Rigidbody rigibody = GetComponent<Rigidbody>();
            rigibody.AddForce(laserDirection * Speed, ForceMode.Impulse);

            transform.forward = laserDirection;

            _enemy = parentEmeny;

            Destroy(gameObject, 2f);
        }


        public void ReturnDamage(float damage)
        {
            _enemy.GetComponent<IHealth>()?.TakeDamage(damage);
        }


        private void OnTriggerEnter(Collider collision)
        {
            if (!_isCollidet && collision.gameObject.CompareTag("Player"))
            {
                IHealth health = collision.transform.GetComponent<IHealth>();

                health?.TakeDamage(_damage);

                if (health.ReturnDamage)
                {
                    ReturnDamage(health.ReturnedDamage);
                }

                Destroy(gameObject);
                _isCollidet = true;
            }
            else if (!_isCollidet && collision.gameObject.CompareTag("Enviroment"))
            {
                Destroy(gameObject);
            }
            else if (!_isCollidet && collision.gameObject.CompareTag("PlayerShield"))
            {
                ShieldPrefab shieldPrefab = collision.gameObject.GetComponentInChildren<ShieldPrefab>();

                shieldPrefab.HitShield();

                if (shieldPrefab.ReturnDamage)
                {
                    ReturnDamage(shieldPrefab.ReturnedDamage);
                }

                Destroy(gameObject);
                _isCollidet = true;

            }
        }
    }
}