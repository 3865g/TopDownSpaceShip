using Scripts.Data;
using Scripts.Infrastructure.States;
using Scripts.Services;
using Scripts.Services.PersistentProgress;
using Scripts.UI.Services.Windows;
using UnityEngine;

namespace Scripts.Logic.Gates
{
    public class FinishScreenGate : MonoBehaviour , IGatesStatus
    {


        public string Button1Text = "menu_mainMenu_button";
        public string ChoiseHeadding = "menu_finish_screen_heading";
        public string ChoiseBody = "menu_finish_screen_body";

        private string _mainMenu = "MainMenu";


        private IGameStateMachine _gameStateMachine;
        private IWindowService _windowService;
        private IPersistentProgressService _persistentProgressService;


        private void Awake()
        {
            _gameStateMachine = AllServices.Container.Single<IGameStateMachine>(); ;
            _windowService = AllServices.Container.Single<IWindowService>();
            _persistentProgressService = AllServices.Container.Single<IPersistentProgressService>();
        }

        public void UpdateStatus()
        {
            CreateChoiseWindow();
        }

        public void CreateChoiseWindow()
        {
            _windowService.Open(WindowId.Confim);
            _windowService.ConfimWindow.Construct(Button1Text, ChoiseHeadding, ChoiseBody);
            _windowService.ConfimWindow.Choice1 += GameOver;
        }


        public void GameOver()
        {
            _persistentProgressService.Progress = NewProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_mainMenu);
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