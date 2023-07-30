using Scripts.Enemy;
using Scripts.Hero;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.Hero
{
    public class HeroAttackZone : MonoBehaviour
    {
        public LayerMask LayerMask;

        private TriggerObserver _triggerObserver;
        private HeroAttack _heroAttack;
        private RotateForAttack _rotateForaAttack;
        private ShipMove _shipMove;
        private GameObject _target;
        private bool _getTarget;

        private void Awake()
        {
            _triggerObserver = GetComponentInChildren<TriggerObserver>();
            _heroAttack = GetComponent<HeroAttack>();
            _rotateForaAttack = GetComponent<RotateForAttack>();
            _shipMove = GetComponent<ShipMove>();
        }


        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

        }

       




        private void TriggerEnter(Collider obj)
        {
            if (obj.CompareTag("CanHit") && !_getTarget)
            {
                _heroAttack.CanAttack = true;
                _target = obj.gameObject;
                _rotateForaAttack.GetTarget(_target.gameObject);
                _getTarget = true;
                //Debug.Log("Hero attack zone trigger enter");

            }
        }

        private void TriggerExit(Collider obj)
        {
            if (obj.CompareTag("CanHit") && _getTarget)
            {
                _rotateForaAttack.GetTarget(null);
                _heroAttack.CanAttack = false;
                _shipMove.RotateToEnemy = false;
                _getTarget = false;
            }
            //Debug.Log(obj);

        }



    }
}