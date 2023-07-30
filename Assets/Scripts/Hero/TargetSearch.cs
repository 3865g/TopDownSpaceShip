using Assets.Scripts.Hero;
using Scripts.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Scripts.Hero
{
    public class TargetSearch : MonoBehaviour
    {
        private const string UpdateTargetString = "UpdateTarget";
        private const string CanHit = "Enemy";
        public Transform target;
        public float range = 40f;
        public LayerMask LayerMask;

        private GameObject _nearestEnemy;
        private RotateForAttack _rotateForAttack;
        private int _layerMask;
        private Collider[] _hits = new Collider[10];
        private HeroAttack _heroAttack;

        private void Awake()
        {
            _rotateForAttack = GetComponent<RotateForAttack>();
            _heroAttack = GetComponent<HeroAttack>();
            _layerMask = LayerMask;
        }

        private void Start()
        {
            InvokeRepeating(UpdateTargetString, 0f, 0.5f);
        }

        private void Update()
        {
            if(target == null)
            {
                return;
            }
            else
            {
                target = null;
            }
        }

        public void UpdateTarget()
        {
            float shortedDistance = Mathf.Infinity;
            _nearestEnemy = null;
            for (int i = 0; i < Hit(); i++)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, _hits[i].transform.position);
                if(distanceToEnemy < shortedDistance)
                {
                    shortedDistance = distanceToEnemy;
                    _nearestEnemy = _hits[i].transform.parent.gameObject;
                }
            }

            if(_nearestEnemy != null && shortedDistance <= range)
            {
                target = _nearestEnemy.transform;
                _rotateForAttack.GetTarget(_nearestEnemy);
                _heroAttack.CanAttack = true;
            }
            else
            {
                _rotateForAttack.GetTarget(null);
                _heroAttack.CanAttack = false;
            }
        }
        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(transform.position, range, _hits, _layerMask);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, range);
        }

    }
}