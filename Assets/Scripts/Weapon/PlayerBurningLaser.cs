using Scripts.Logic;
using Scripts.Weapon;
using UnityEngine;

namespace Scripts.Hero
{
    public class PlayerBurningLaser : MonoBehaviour, IProjectile
    {
        public float Speed = 500f;

        public float DamageAmount;
        public float DamageInterval;
        public int DamgaeDuration;

        public Color Color { get; set; }


        public Transform Target;
        public GameObject Impact;
        public GameObject BurningModule;

        private bool _isCollidet;
        private float _damage;
        private GameObject _burningModule;

        private void Start()
        {
            transform.LookAt(Target);
        }


        public void Construct(Vector3 Direction, float damage, Color color)
        {
            _damage = damage;
            // Debug.Log(_damage);
            Rigidbody rigibody = GetComponent<Rigidbody>();
            rigibody.AddForce(Direction * Speed, ForceMode.Impulse);

            transform.forward = Direction;

            Destroy(gameObject, 2f);
        }



        private void OnTriggerEnter(Collider collision)
        {
            Vector3 impactTransform = gameObject.transform.position;

            if (!_isCollidet && collision.gameObject.CompareTag("CanHit"))
            {
                collision.transform.parent.GetComponent<IHealth>()?.TakeDamage(_damage);

                if (collision.gameObject.GetComponentInChildren<BurningDamage>() == null)
                {
                    _burningModule = Instantiate(BurningModule, collision.transform);
                    _burningModule.transform.SetParent(collision.transform);
                    BurningDamage burningDamage = _burningModule.GetComponent<BurningDamage>();
                    burningDamage.Construct(collision.gameObject);
                }
                else
                {
                    _burningModule.GetComponent<BurningDamage>().DamgaeDuration = DamgaeDuration;
                }



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
            GameObject impact = Instantiate(Impact, transform, Quaternion.identity);
            Destroy(impact, 0.5f);

        }


    }
}