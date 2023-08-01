using Scripts.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    public class AgentSearchPlayer : Follow
    {
        private Transform _heroTransfom;


        internal void Construct(Transform heroTransform)
        {
            _heroTransfom = heroTransform;
        }       
    }
}