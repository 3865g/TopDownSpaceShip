using Scripts.Data;
using Scripts.Infrastructure.States;
using Scripts.Services.Input;
using Scripts.Services;
using Scripts.Services.PersistentProgress;
using Scripts.UI.Services.Windows;
using UnityEngine;

namespace Scripts.Logic.Gates
{
    public class FinishScreenGate : MonoBehaviour , IGatesStatus
    {


        public string Button1Text = "main menu";
        public string ChoiseHeadding = "You have cleared this planet";
        public string ChoiseBody = "You have finished your business on this planet. It's time to go to the base for new tasks.";

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
            _windowService.ConfimWindow.Button1Text.text = Button1Text;
            _windowService.ConfimWindow.MainTextHeading.text = ChoiseHeadding;
            _windowService.ConfimWindow.MainTextBody.text = ChoiseBody;
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