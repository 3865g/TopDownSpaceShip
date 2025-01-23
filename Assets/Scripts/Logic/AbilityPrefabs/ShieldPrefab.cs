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
        public GameObject LineRenderer;

        private GameObject[] _lineRenderers;

        private int _damageWaveAmount;
        private bool _damageWave;
        private float _radius;
        private Vector3 _closestPointOnSphere;


      

        internal void Construct( bool returnDamage, bool damageWave, float returnedDamage, int damageWaveAmount, AbilityHolder abilityHolder)
        {
            ReturnDamage = returnDamage;
            ReturnedDamage = returnedDamage;
            _damageWave = damageWave;
            _damageWaveAmount = damageWaveAmount;
            AbilityHolder = abilityHolder;
        }

        private void Awake()
        {
            _radius = gameObject.GetComponent<SphereCollider>().radius;
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
                        CreateLineRenders(collider.gameObject);
                        collider.transform.parent.GetComponent<IHealth>()?.TakeDamage(_damageWaveAmount, Color.blue);
                    }
                }
                //Debug.LogError("Play DamageWave FX");

                GameObject shockWave = Instantiate(ShockWaveFx, gameObject.transform);
                Destroy(shockWave, 0.5f);
            }
        }

        public void CreateLineRenders(GameObject enemy)
        {
            Vector3 targetPosition = enemy.transform.position;  
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();

            _closestPointOnSphere = transform.position + direction * _radius;

            GameObject hitShock = Instantiate(LineRenderer);
            LineRenderer lineRenderer = hitShock.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, _closestPointOnSphere);
            lineRenderer.SetPosition(1, targetPosition);


            Destroy(hitShock, 0.2f);




        }
    }
}


