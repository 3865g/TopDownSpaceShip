using Scripts.Infrastructure.States;
using Scripts.Services.SaveLoad;
using Scripts.Services;
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
        private ISaveLoadService _saveLoadService;


        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }


        private void OnEnable()
        {
            _gameStateMachine = GetComponentInParent<LevelsMenu>()._gameStateMachine;
            Button.onClick.AddListener(ChangeLevel);

        }

        public void ChangeLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(TransferTo);
        }
    }
}