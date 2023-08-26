using UnityEngine;
using System;
using Cinemachine;

namespace Scripts.CameraLogic
{
    class FollowPlayer : MonoBehaviour
    {
        public float RotationAngleX;
        public float Distance;
        public float OffsetY;

        public CinemachineBrain cinemachineBrain;

        [SerializeField]
        private Transform _followingTarget;

        private void Start()
        {
            cinemachineBrain = GetComponent<CinemachineBrain>();
            
        }

        private void LateUpdate()
        {
            if (_followingTarget == null)
            {
                return;
            }

                Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);
                Vector3 followingPosition = FollowingPoinPosition();
                Vector3 position = rotation * new Vector3(0, 0, -Distance) + followingPosition;

                //transform.rotation = rotation;
                //transform.position = position;
            
        }

        public void FollowObject(GameObject followingObject)
        {
            _followingTarget = followingObject.transform;

            //Transform vCamTransform = new Transform(followingObject.transform.position.x, followingObject.transform.position.y, followingObject.transform.position.z - 20, Quaternion.identity) ;
            Vector3 vCamPos = new Vector3(followingObject.transform.position.x, followingObject.transform.position.y, followingObject.transform.position.z -20);
            cinemachineBrain.ActiveVirtualCamera.Follow = followingObject.transform;
        }

        private Vector3 FollowingPoinPosition()
        {
            Vector3 followingPosition = _followingTarget.position;
            followingPosition.y += OffsetY;

            return followingPosition;
        }
    }
}
