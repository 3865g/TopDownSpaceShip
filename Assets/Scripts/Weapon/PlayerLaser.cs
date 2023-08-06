using Scripts.Logic;
using System;
using UnityEngine;

namespace Scripts.Hero
{
    public class PlayerLaser : MonoBehaviour
    {
        public float Speed = 500f;
        public Transform Target;
        public GameObject Impact;

        private bool _isCollidet;
        private float _damage = 20f;

        private void Start()
        {
            transform.LookAt(Target);
        }


        public void Construct(Vector3 laserDirection, float damage)
        {
            
            //_damage = damage;
            Rigidbody rigibody = GetComponent<Rigidbody>();
            rigibody.AddForce(laserDirection * Speed, ForceMode.Impulse);

            transform.forward = laserDirection;

            Destroy(gameObject, 2f);
        }



        private void OnTriggerEnter(Collider collision)
        {
            Vector3 impactTransform = gameObject.transform.position;

            if (!_isCollidet && collision.gameObject.CompareTag("CanHit"))
            {
                collision.transform.parent.GetComponent<IHealth>()?.TakeDamage(_damage);
                Destroy(gameObject);
                _isCollidet = true;
                ImpactFX(impactTransform);
            }
            else if (!_isCollidet && collision.gameObject.CompareTag("Enviroment"))
            {
                Destroy(gameObject);
                ImpactFX(impactTransform);
            }
        }

        private void ImpactFX(Vector3 transform)
        {
          GameObject impact =  Instantiate(Impact, transform , Quaternion.identity);
            Destroy(impact, 0.5f);

        }
    }
}