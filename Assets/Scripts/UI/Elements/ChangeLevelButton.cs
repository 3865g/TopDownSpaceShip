using Scripts.Data;
using Scripts.Infrastructure.States;
using Scripts.Services;
using Scripts.Services.PersistentProgress;
using Scripts.Services.SaveLoad;
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


        private void Awake()
        {
            _gameStateMachine = GetComponentInParent<LevelsMenu>().GameStateMachine;
            Button.onClick.AddListener(ChangeLevel);
        }

        public void ChangeLevel()
        {

            _gameStateMachine.Enter<LoadLevelState, string>(TransferTo);
        }
    }
}