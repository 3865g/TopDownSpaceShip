using Scripts.Data;
using Scripts.Infrastructure.Factory;
using UnityEngine;

namespace Scripts.Infrastructure.Services.PersistentProgress.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {

        private const string ProgressKey = "Progress";

        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
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

    }
}
