using Scripts.Logic;
using UnityEngine;

namespace Scripts.Hero
{
    public class EnemyFlamethrower : MonoBehaviour
    {
        public new ParticleSystem particleSystem;


        public float DamageAmount;
        public float DamageInterval;
        public int DamgaeDuration;


        public GameObject BurningModule;

        private bool _canDamage;
        private const string PlayerTag = "Player";

        private GameObject _burningModule;

        private bool _isCollidet;
        private float _damage;

        public void Construct(Vector3 laserDirection, float damage)
        {
            _damage = damage;
        }

        void Start()
        {
           particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        public void Enable()
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
                _canDamage = true;
            }
        }

        public void Disable()
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
                _canDamage = false;
            }
        }


        private void OnTriggerEnter(Collider collision)
        {
            if (_canDamage && collision.gameObject.CompareTag("Player"))
            {
                if (collision.transform.GetComponentInChildren<BurningDamage>() == null)
                {
                    _burningModule = Instantiate(BurningModule, collision.transform.parent);
                    _burningModule.transform.SetParent(collision.transform);
                    //Debug.Log(_burningModule);
                    BurningDamage burningDamage = _burningModule.GetComponent<BurningDamage>();
                    burningDamage.Construct(collision.gameObject);
                }
                else
                {
                    collision.transform.GetComponentInChildren<BurningDamage>().DamgaeDuration = DamgaeDuration;
                }
            }
        }
    }
}