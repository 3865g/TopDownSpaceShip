using Scripts.Data;
using Scripts.Infrastructure.Factory;
using Scripts.Services.Ga;
using Scripts.Services.GameSettings;
using Scripts.Services.PersistentProgress;
using UnityEngine;

namespace Scripts.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {

        private const string ProgressKey = "Progress";
        private const string SettingsKey = "Settings";

        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IGameSettingsService _gameSettingsService;

        public SaveLoadService(IPersistentProgressService progressService, IGameSettingsService gameSettingsService, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _gameSettingsService = gameSettingsService;
            _progressService = progressService;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }
        public PlayerProgress LoadProgress()
        {
           return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }

        public void SaveSettings()
        {
            foreach (ISavedSettings settingsWriter in _gameFactory.SettingsWriters)
            {
                settingsWriter.UpdateSettings(_gameSettingsService.GameGlobalSettings);
            }
            PlayerPrefs.SetString(SettingsKey, _gameSettingsService.GameGlobalSettings.ToJson());
        }

        public GameGlobalSettings LoadSettings()
        {
            return PlayerPrefs.GetString(SettingsKey)?.ToDeserialized<GameGlobalSettings>();
        }
    }
}
