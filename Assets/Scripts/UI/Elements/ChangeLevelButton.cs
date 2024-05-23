using Scripts.Infrastructure.States;
using Scripts.UI.Services.Windows;
using Scripts.UI.Windows.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class ChangeLevelButton : MonoBehaviour
    {
        public Button Button;
        public string TransferTo;
        private IGameStateMachine _gameStateMachine;

        //public void Construct(IGameStateMachine gameStateMachine)
        //{
        //    _gameStateMachine = gameStateMachine;
        //}

        //private void Awake()
        //{
        //    Button.onClick.AddListener(ChangeLevel);
            
        //}

        private void OnEnable()
        {
            _gameStateMachine = GetComponentInParent<LevelsMenu>()._gameStateMachine;
            Button.onClick.AddListener(ChangeLevel);
            
        }

        public void ChangeLevel()
        {
            Debug.Log(_gameStateMachine);
            _gameStateMachine.Enter<LoadLevelState, string>(TransferTo);
            Debug.Log(TransferTo);
        }
    }
}