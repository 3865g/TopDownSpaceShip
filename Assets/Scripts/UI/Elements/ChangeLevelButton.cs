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
        public LevelsMenu Levelmenu;
        private IWindowService _windowService;
        private IGameStateMachine _gameStateMachine;

        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

        private void Awake()
        {
            Button.onClick.AddListener(ChangeLevel);
            
        }

        public void ChangeLevel()
        {
            _gameStateMachine = Levelmenu._gameStateMachine;
            Debug.Log(_gameStateMachine);
            _gameStateMachine.Enter<LoadLevelState, string>(TransferTo);
        }
    }
}