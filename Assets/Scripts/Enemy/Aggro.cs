using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public float cooldown = 1f;

        private TriggerObserver _triggerObserver;
        private Follow _follow;
        private Coroutine _aggroCooutine;
        private RotateToHero _rotateToHero;
        private Ray _ray;

        private bool _hasAggroTargget;

        private void Awake()
        {
            _triggerObserver = GetComponentInChildren<TriggerObserver>();
            _follow = GetComponentInChildren<Follow>();
            _rotateToHero = GetComponentInChildren<RotateToHero>();



        }

        private void Start()
        {

            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }


        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTargget)
            {
                //_ray = new Ray(transform.position, obj.transform.position);
                //Physics.Raycast(_ray, out RaycastHit hit);


                //if (hit.collider.tag != "Enviroment")
                //{
                //    Debug.Log(hit.collider.gameObject.name);
                //}

                // if (hit == Player) {}
                _hasAggroTargget = true;
                _rotateToHero.IsCollided = _hasAggroTargget;

                StopAggroCorutine();
                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTargget)
            {
                _hasAggroTargget = false;
                _rotateToHero.IsCollided = _hasAggroTargget;

                _aggroCooutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(cooldown);

            SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            _follow.enabled = true;
        }
        private void SwitchFollowOff()
        {
                _follow.enabled = false;
                        
        }
        private void StopAggroCorutine()
        {
            _aggroCooutine = null;
        }

    }
}