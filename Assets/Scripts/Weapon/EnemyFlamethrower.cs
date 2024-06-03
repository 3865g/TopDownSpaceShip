using Scripts.Logic;
using UnityEngine;

namespace Scripts.Hero
{
    public class EnemyFlamethrower : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        //public float Speed = 150f;
        //public Transform Target;
        public ParticleSystem particleSystem;
        //public List<ParticleCollisionEvent> collisionEvents;

        private bool _isCollidet;
        private float _damage;

        //private void Start()
        //{
        //    transform.LookAt(Target);
        //}


        public void Construct(Vector3 laserDirection, float damage)
        {
            _damage = damage;
        }

        void Start()
        {
           particleSystem = GetComponentInChildren<ParticleSystem>();
            //collisionEvents = new List<ParticleCollisionEvent>();
        }

        public void Enable()
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }

        public void Disable()
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }

        //private void OnParticleCollision(GameObject other)
        //{
        //    if (other.tag == PlayerTag)
        //    {
        //        other.transform.GetComponent<IHealth>()?.TakeDamage(_damage);
        //        Debug.Log("fire damage");
        //    }
        //}


        private void OnTriggerEnter(Collider collision)
        {
            if (!_isCollidet && collision.gameObject.CompareTag("Player"))
            {
                collision.transform.GetComponent<IHealth>()?.TakeDamage(_damage);
                //Destroy(gameObject);
                _isCollidet = true;
                Debug.Log("FireCollision");
            }
        }
    }
}