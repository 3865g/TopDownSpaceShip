using Scripts.CameraLogic;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public float cooldown = 1f;

        private TriggerObserver _triggerObserver;
        private Follow _follow;
        private Coroutine _aggrpCooutine;

        private bool _hasAggroTargget;


        private void Start()
        {
            _triggerObserver = GetComponentInChildren<TriggerObserver>();
            _follow = GetComponentInChildren<Follow>();

            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }


        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTargget)
            {
                _hasAggroTargget = true;

                StopAggroCorutine();
                SwitchFollowOn();
            }
        }        

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTargget)
            {
                _hasAggroTargget = false;
                _aggrpCooutine =  StartCoroutine(SwitchFollowOffAfterCooldown());
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
            _aggrpCooutine = null;
        }

    }
}