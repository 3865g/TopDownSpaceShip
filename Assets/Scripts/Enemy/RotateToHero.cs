using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
    public class RotateToHero : Follow
    {
        public float speed = 15f;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

        void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            if (HeroExists())
            {
                InitialHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += InitialHeroTransform;
            }

        }

        void Update()
        {
            if(Initialized())
            {
                RotateTowardshero();
            }
        }

        private void RotateTowardshero()
        {
            UpdatePositionLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private float SpeedFactor()
        {
           return speed * Time.deltaTime;
        }

        private Quaternion TargetRotation(Vector3 positionToLook)
        {
            return Quaternion.LookRotation(positionToLook);
        }

        private void UpdatePositionLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        }

        private bool Initialized()
        {
            return _heroTransform != null;
        }

        private void InitialHeroTransform()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }

        private bool HeroExists()
        {
            return _gameFactory.HeroGameObject != null;
        }

        
    }
}