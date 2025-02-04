using Scripts.Data;
using Scripts.Services.PersistentProgress;

namespace Scripts.Services.Ga
{
    public interface ISavedSettings : ISavedSettingsReader
    {
        void UpdateSettings(GameGlobalSettings gameSettings);
    }
}
