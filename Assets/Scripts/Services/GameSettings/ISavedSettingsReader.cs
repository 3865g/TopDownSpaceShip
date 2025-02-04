using Scripts.Data;

namespace Scripts.Services.PersistentProgress
{
    public interface ISavedSettingsReader
    {
        void LoadSettings(GameGlobalSettings gameSettings);
    }
}
