using System.Collections;
using UnityEngine;


namespace Scripts.Logic
{
    public class HPRegeneration : MonoBehaviour
    {
        public float RegenerationAmount;
        public float RegenerationInterval;

        private IHealth _health;

        public void Construct(GameObject parent)
        {
            _health = parent.GetComponent<IHealth>();
            StartCoroutine(RegenerationHP());
        }

        public IEnumerator RegenerationHP()
        {
            while (true)
            {
                _health.TakeHP(RegenerationAmount);

                yield return new WaitForSeconds(RegenerationInterval);
            }
        }
    }
}
