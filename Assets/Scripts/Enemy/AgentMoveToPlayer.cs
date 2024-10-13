using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        public  NavMeshAgent _agent;
        private GameObject _heroTransfom;
        private float _updateTime;

        public Vector3 HeroTransform;


        internal void Construct(GameObject heroTransform)
        {
            _heroTransfom = heroTransform;
        }

        

        private void Start()
        {
            StartCoroutine(SetDestinationForAgent());
        }



        private void Update()
        {
            if (_heroTransfom)
            {
                _agent.SetDestination(_heroTransfom.transform.position);
            }
            HeroTransform = _heroTransfom.transform.position;
        }



        private IEnumerator SetDestinationForAgent()
        {
            WaitForSeconds wait = new WaitForSeconds(_updateTime);

            while (enabled)
            {
                //if (_heroTransfom)
                //{
                //    _agent.SetDestination(_heroTransfom.transform.position);
                //}
                //    HeroTransform = _heroTransfom.transform.position;
                yield return wait;
            }
        }        
    }
}