using Scripts.Hero.Ability;
using UnityEngine;

namespace Scripts.Logic
{
    public class 
        ShieldPrefab : MonoBehaviour
    {
        public bool ReturnDamage;
        public float ReturnedDamage;
        public float DamageWaveRadius = 100f;
        
        public AbilityHolder AbilityHolder;

        public GameObject ShockWaveFx;


        private int _damageWaveAmount;
        private bool _damageWave;


      

        internal void Construct( bool returnDamage, bool damageWave, float returnedDamage, int damageWaveAmount, AbilityHolder abilityHolder)
        {
            ReturnDamage = returnDamage;
            ReturnedDamage = returnedDamage;
            _damageWave = damageWave;
            _damageWaveAmount = damageWaveAmount;
            AbilityHolder = abilityHolder;
        }

        public void HitShield()
        {
            if (_damageWave)
            {
                Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, DamageWaveRadius);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("CanHit"))
                    {
                        collider.transform.parent.GetComponent<IHealth>()?.TakeDamage(_damageWaveAmount);
                    }
                }
                //Debug.LogError("Play DamageWave FX");

                GameObject shockWave = Instantiate(ShockWaveFx, gameObject.transform);
                Destroy(shockWave, 0.5f);
            }
        }
    }
}


