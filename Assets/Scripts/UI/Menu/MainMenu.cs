using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.States;
using Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public Button Button;

        public GameObject AbilityManager;
        private IWindowService _windowService;

        private IGameStateMachine _gameStateMachine;
        private IGameFactory _gameFactory;

        public void Construct(IGameStateMachine gameStateMachine, IWindowService windowService)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;

            GetComponentInChildren<StartLevelButton>().Construct(_gameStateMachine, _windowService);
        }


    }
}