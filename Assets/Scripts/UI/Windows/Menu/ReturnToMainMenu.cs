
using Scripts.Data;
using Scripts.Infrastructure.States;
using Scripts.Services;
using Scripts.Services.PersistentProgress;
using Scripts.Services.SaveLoad;
using Scripts.UI.Services.Windows;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Menu
{

    public class ReturnToMainMenu : WindowBase
    {
        public Button Button;


        public string ChoiseButton1 = "Cancel";
        public string ChoiseButton2 = "Confirm";
        public string ChoiseHeadding = "Do you want to return to the main menu?";
        public string ChoiseBody = "If you return to the main menu, the current playthrough will end.";

        private string _mainMenu = "MainMenu";

        private IWindowService _windowService;

        private IGameStateMachine _gameStateMachine;

        private ISaveLoadService _saveLoadService;

        private IPersistentProgressService _persistentProgressService;


        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _persistentProgressService = AllServices.Container.Single<IPersistentProgressService>();
            _windowService = AllServices.Container.Single<IWindowService>();

            Button.onClick.AddListener(ClickStart);
        }

        public void ClickStart()
        {
            CreateChoiseWindow();
        }

        public void CreateChoiseWindow()
        {
            _windowService.Open(WindowId.ChoiceWindow);
            _windowService.ChoiceWindow.Button1Text.text = ChoiseButton1;
            _windowService.ChoiceWindow.Button2Text.text = ChoiseButton2;
            _windowService.ChoiceWindow.MainTextHeading.text = ChoiseHeadding;
            _windowService.ChoiceWindow.MainTextBody.text = ChoiseBody;
            _windowService.ChoiceWindow.Choice1 += Cancel;
            _windowService.ChoiceWindow.Choice2 += Confirm;
            _windowService.ChoiceWindow.DestroyWindow += DestroyWindow;
        }


        public void Cancel()
        {
            DestroyWindow();
        }

        public void Confirm()
        {
            _persistentProgressService.Progress = NewProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_mainMenu);
        }

        public void DestroyWindow()
        {
            _windowService.ChoiceWindow.Choice1 -= Cancel;
            _windowService.ChoiceWindow.Choice2 -= Confirm;
            _windowService.ChoiceWindow.DestroyWindow -= DestroyWindow;
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
