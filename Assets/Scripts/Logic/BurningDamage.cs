using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;


namespace Scripts.Logic
{
    public class BurningDamage : MonoBehaviour
    {
        public GameObject BurnFx;
        public float DamageAmount = 5;
        public float DamageInterval = 1;
        public int DamgaeDuration = 5;

        private IHealth _health;
        private ParticleSystem _burningFX;
        private Transform _parent;

        public void Construct(GameObject parent)
        {
            _parent = parent.transform;
            _health = parent.transform.GetComponentInParent<IHealth>();
            Debug.Log(_health);
            StartCoroutine(Burning());
        }

        public IEnumerator Burning()
        {
            while (DamgaeDuration > 0)
            {
                _health.TakeDamage(DamageAmount, Color.yellow);

                _burningFX = BurnFx.GetComponent<ParticleSystem>();
                var sh = _burningFX.shape;
                if (_parent.GetComponent<HurtBox>())
                {
                    sh.radius = _parent.GetComponent<HurtBox>().Radius;
                }
                else 
                {
                    sh.radius = 5f;
                }

                _burningFX.Play();

                //Burn FX enable

                DamgaeDuration--;

                yield return new WaitForSeconds(DamageInterval);
            }
            //Burn FX disable
            _burningFX.Stop();

            Destroy(gameObject);
        }
    }
}
