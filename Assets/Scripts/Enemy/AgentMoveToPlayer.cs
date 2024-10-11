using Scripts.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        public  NavMeshAgent _agent;
        private Transform _heroTransfom;


        internal void Construct(Transform heroTransform)
        {
            _heroTransfom = heroTransform;
        }


        private void Update()
        {
            SetDestinationForAgent();
        }

        private void SetDestinationForAgent()
        {

            if (_heroTransfom)
            {
                _agent.destination = _heroTransfom.position;
            }
        }        
    }
}