using Assets.Scripts.UI.Menu;
using Scripts.Infrastructure.States;
using Scripts.Services.PersistentProgress;
using Scripts.Services;
using System;
using TMPro;
using UnityEngine.UI;
using Scripts.Data;

namespace Scripts.UI.Windows.Menu
{
    public class GameOverWindow : WindowBase
    {

        public Button Button1;

        public TextMeshProUGUI Button1Text;
        public TextMeshProUGUI MainTextHeading;
        public TextMeshProUGUI MainTextBody;

        private IGameStateMachine _gameStateMachine;
        private IPersistentProgressService _persistentProgressService;

        private string _mainMenu = "MainMenu";


        public void Construct(IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
        }

        private void Awake()
        {
            Button1.onClick.AddListener(Button1Click);
        }

        public void Button1Click()
        {
            _persistentProgressService.Progress = NewProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_mainMenu);
        }
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel: _mainMenu);

            //Need Refactoring, load vcalues from SO

            progress.HeroStats.Damage = 10;
            progress.HeroStats.DamageRadius = 20;
            progress.HeroState.MaxHP = 50;
            progress.HeroState.ResetHP();



            return progress;
        }
    }
}
