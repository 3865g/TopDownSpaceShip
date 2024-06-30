using System.Collections;
using UnityEngine;


namespace Scripts.Logic
{
    public class BurningDamage : MonoBehaviour
    {
        public float DamageAmount;
        public float DamageInterval;
        public int DamgaeDuration;

        private IHealth _health;

        public void Construct(GameObject parent)
        {
            _health = parent.GetComponent<IHealth>();
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
