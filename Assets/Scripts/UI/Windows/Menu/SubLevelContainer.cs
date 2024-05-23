using Assets.Scripts.UI.Elements;
using Scripts.Infrastructure.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Windows.Menu
{
    public class SubLevelContainer : MonoBehaviour
    {


        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        private void Awake()
        {
            //gameObject.SetActive(false);
        }
        

        public void Enable()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
               // Debug.Log("Active");
            }
            else
            {
                gameObject.SetActive(true);
                //Debug.Log("Deactive");
            }
            
        }

        
    }
}
