using Scripts.Logic;
using Scripts.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Hero
{
    public class PlayerLaser : MonoBehaviour, IProjectile
    {
        public float Speed = 500f;
        public Transform Target;
        public GameObject Impact;
        public Renderer Render;

        public Color Color {  get; set; }

        private bool _isCollidet;
        private float _damage;

        private void Start()
        {
            transform.LookAt(Target);            
        }


        public void Construct(Vector3 laserDirection, float damage, Color color)
        {

            Color = color;
            if(laserDirection == null)
            {
                laserDirection = transform.forward;
            }
            _damage = damage;
           // Debug.Log(_damage);
            Rigidbody rigibody = GetComponent<Rigidbody>();
            rigibody.AddForce(laserDirection * Speed, ForceMode.Impulse);

            
            Render.material.SetColor("_BaseColor", color );

            transform.forward = laserDirection;

            Destroy(gameObject, 2f);
        }



        private void OnTriggerEnter(Collider collision)
        {
            Vector3 impactTransform = gameObject.transform.position;

            if (!_isCollidet && collision.gameObject.CompareTag("CanHit"))
            {
                collision.transform.parent.GetComponent<IHealth>()?.TakeDamage(_damage, Color.red);
                IHealth health = collision.transform.parent.GetComponent<IHealth>();
                //Debug.Log(health);
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