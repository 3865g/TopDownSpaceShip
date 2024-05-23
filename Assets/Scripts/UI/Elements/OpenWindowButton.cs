using Scripts.Infrastructure.States;
using Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        public WindowId WindowId;
        public Button Button;
        private IWindowService _windowService;
        private IGameStateMachine _gameStateMachine;

        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

        private void Awake()
        {
            Button.onClick.AddListener(Open);
            //Button = GetComponent<Button>();
        }

        private void Open()
        {
            _windowService.Open(WindowId);
            Debug.Log(WindowId);
        }
    }
}