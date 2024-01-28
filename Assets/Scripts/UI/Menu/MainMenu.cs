using Scripts.Infrastructure.States;
using Scripts.Services;
using Scripts.Services.Input;
using Scripts.UI.Services.Windows;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public Button Button;
        private IWindowService _windowService;

        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine, IWindowService windowService)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
        }

        //private void Awake()
        //{
        //    Button.onClick.AddListener(ChangeLevel);
        //}


        //public void StartLevel()
        //{
        //    Button.onClick.AddListener(ChangeLevel);
        //        //_gameStateMachine.Enter<LoadLevelState, string>(TransferTo);
        //}

        //public void ChangeLevel()
        //{
        //    Debug.Log("ChangeLevel");
        //    _gameStateMachine.Enter<LoadLevelState, string>("L1P1");
        //}
    }
}