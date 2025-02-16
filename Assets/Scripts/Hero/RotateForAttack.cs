using Scripts.Hero;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Hero
{
    public class RotateForAttack : MonoBehaviour
    {
        public float speed = 15f;

        public GameObject _enemy;
        private Vector3 _positionToLook;
        private ShipMove _shipMove;
        private Rigidbody _rigidbody;

        public void GetTarget(GameObject enemyTransform)
        {
            _enemy = enemyTransform;
        }

        private void Awake()
        {
            _shipMove = GetComponent<ShipMove>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (Initialized())
            {
                RotateTowardsEnemy();
            }
            else
            {
                RotateTowards();
            }
           
            //Debug.Log(_enemyTransform.gameObject.SetActive == true);

        }

        private void RotateTowardsEnemy()
        {
            UpdatePositionLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void RotateTowards()
        {
            transform.rotation = SmoothedRotation(transform.rotation, _shipMove.MovementVector);
        }

        private void UpdatePositionLookAt()
        {
            Vector3 positionDiff = _enemy.transform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, positionDiff.y, positionDiff.z);
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



        private bool Initialized()
        {
            return _enemy != null;
        }
    }
}