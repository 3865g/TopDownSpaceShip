using Scripts.Data;

namespace Scripts.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        void SaveSettings();
        PlayerProgress LoadProgress();
        GameGlobalSettings LoadSettings();
    }
}
