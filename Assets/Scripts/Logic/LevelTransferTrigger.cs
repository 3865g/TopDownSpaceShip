﻿using Scripts.Infrastructure.States;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        public string TransferTo;

        private const string PlayerTag = "Player";
        private IGameStateMachine _gameStateMachine;
        private bool _isTransfering = false;
               

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (_isTransfering)
            {
                return;
            }

            if (other.CompareTag(PlayerTag))
            {
                    Debug.Log("Transfer");
                    _gameStateMachine.Enter<LoadLevelState, string>(TransferTo);
                    _isTransfering = true;
             }

        }
    }
}
