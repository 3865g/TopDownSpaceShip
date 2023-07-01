using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private  NavMeshAgent _agent;
        private Transform _heroTransfom;
        private const float _stopFollowDistance = 5f;

        private IGameFactory _gameFactory;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();  
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if(_gameFactory.HeroGameObject != null)
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += HeroCreated;
            }
        }


        private void Update()
        {
            if (InitializedHero() && ComparingFollowDistance())
            {
                _agent.destination = _heroTransfom.position;
            }
        }

        

        private bool ComparingFollowDistance()
        {
            return Vector3.Distance(_agent.transform.position, _heroTransfom.position) >= _stopFollowDistance;
        }
        private void HeroCreated()
        {
            InitializeHeroTransform();
        }

        private void InitializeHeroTransform()
        {
            _heroTransfom = _gameFactory.HeroGameObject.transform;
        }
        private bool InitializedHero()
        {
            return _heroTransfom != null;
        }
    }
}