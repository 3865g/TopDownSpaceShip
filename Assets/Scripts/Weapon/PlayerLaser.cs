using Scripts.Logic;
using System;
using UnityEngine;

namespace Scripts.Hero
{
    public class PlayerLaser : MonoBehaviour
    {
        public float Speed = 500f;
        public Transform Target;

        private bool _isCollidet;
        private float _damage;

        private void Start()
        {
            transform.LookAt(Target);
        }


        public void Construct(Vector3 laserDirection, float damage)
        {
            _damage = damage;
            Rigidbody rigibody = GetComponent<Rigidbody>();
            rigibody.AddForce(laserDirection * Speed, ForceMode.Impulse);

            transform.forward = laserDirection;

            Destroy(gameObject, 2f);
        }



        private void OnTriggerEnter(Collider collision)
        {
            if (!_isCollidet && collision.gameObject.CompareTag("CanHit"))
            {
                collision.transform.parent.GetComponent<IHealth>()?.TakeDamage(_damage);
                Destroy(gameObject);
                _isCollidet = true;
            }
        }
    }
}