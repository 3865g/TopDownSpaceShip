using System.Collections;
using UnityEngine;


namespace Scripts.Logic
{
    public class BurningDamage : MonoBehaviour
    {
        public float DamageAmount = 5;
        public float DamageInterval = 1;
        public int DamgaeDuration = 5;

        private IHealth _health;

        public void Construct(GameObject parent)
        {
            _health = parent.transform.GetComponentInParent<IHealth>();
            Debug.Log(_health);
            StartCoroutine(Burning());
        }

        public IEnumerator Burning()
        {
            while (DamgaeDuration > 0)
            {
                _health.TakeDamage(DamageAmount);

                //Burn FX enable

                DamgaeDuration--;

                yield return new WaitForSeconds(DamageInterval);
            }
            //Burn FX disable

            Destroy(gameObject);
        }
    }
}
