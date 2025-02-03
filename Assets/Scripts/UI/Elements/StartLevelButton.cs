using Scripts.Infrastructure.States;
using Scripts.Services.SaveLoad;
using Scripts.Services;
using Scripts.UI.Services.Windows;
using UnityEngine.UI;
using Scripts.Services.PersistentProgress;
using Scripts.UI.Windows;
using Scripts.Data;

namespace Assets.Scripts.UI.Menu
{
    public class StartLevelButton : WindowBase
    {
        public Button Button;


        public string ChoiseButton1 = "menu_conture";
        public string ChoiseButton2 = "menu_new_travel";
        public string ChoiseHeadding = "menu_new_travel_heading";
        public string ChoiseBody = "menu_new_travel_body";

        private IWindowService _windowService;

        private IGameStateMachine _gameStateMachine;

        private ISaveLoadService _saveLoadService;

        private IPersistentProgressService _persistentProgressService;


        public string InitialLevel = "MainMenu";


        public void Construct(IGameStateMachine gameStateMachine, IWindowService windowService)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
        }

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _persistentProgressService = AllServices.Container.Single<IPersistentProgressService>();

            Button.onClick.AddListener(ClickStart);
        }


        public void ClickStart()
        {

            if (_saveLoadService.LoadProgress() == null)
            {
                NewGame();
            }
            else
            {
                if (_saveLoadService.LoadProgress().WorldData.PositionOnLevel.SavedLevel == InitialLevel)
                {
                    NewGame();
                }
                else
                {
                    CreateChoiseWindow();
                }
                    
            }
            

        }


        public void CreateChoiseWindow()
        {
            _windowService.Open(WindowId.ChoiceWindow);
            _windowService.ChoiceWindow.Construct(ChoiseButton1, ChoiseButton2, ChoiseHeadding, ChoiseBody);
            _windowService.ChoiceWindow.Choice1 += ContinueGame;
            _windowService.ChoiceWindow.Choice2 += NewGame;
            _windowService.ChoiceWindow.DestroyWindow += DestroyWindow;
        }

        public void ContinueGame()
        {
            _persistentProgressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

            _gameStateMachine.Enter<LoadLevelState, string>(_persistentProgressService.Progress.WorldData.PositionOnLevel.SavedLevel);
        }

        public void NewGame()
        {
            _persistentProgressService.Progress = NewProgress();
            _windowService.Open(WindowId.LevelsMenu);
        }


        //NeedRefactoring?
        public PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel: InitialLevel);

            //Need Refactoring, load vcalues from SO

            progress.HeroStats.Damage = 10;
            progress.HeroStats.DamageRadius = 20;
            progress.HeroState.MaxHP = 50;
            progress.HeroState.ResetHP();



            return progress;
        }

        public void DestroyWindow()
        {
            _windowService.ChoiceWindow.Choice1 -= ContinueGame;
            _windowService.ChoiceWindow.Choice2 -= NewGame;
            _windowService.ChoiceWindow.DestroyWindow -= DestroyWindow;
        }
    }
}