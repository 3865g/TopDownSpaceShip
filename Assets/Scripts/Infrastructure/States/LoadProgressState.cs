using Scripts.Services.PersistentProgress;
using Scripts.Data;
using Scripts.Services.SaveLoad;
using Scripts.Services.GameSettings;

namespace Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        public string InitialLevel = "MainMenu";
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly IGameSettingsService _gameSettingsService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, IGameSettingsService gameSettingsService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _gameSettingsService = gameSettingsService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            LoadSettingsOrInitNew();


            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }


        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
            
        }

        private void LoadSettingsOrInitNew()
        {
            _gameSettingsService.GameGlobalSettings = _saveLoadService.LoadSettings() ?? NewSettings();

        }

        private GameGlobalSettings NewSettings()
        {
            var settings = new GameGlobalSettings();
            settings.LocalSettings.LocalID = 1;
            return settings;
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel : InitialLevel);

            progress.HeroStats.Damage = 10;
            progress.HeroStats.DamageRadius = 20;
            progress.HeroState.MaxHP = 50;
            progress.HeroState.ResetHP();



            return progress;
        }
    }
}
