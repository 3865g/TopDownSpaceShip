using Scripts.Data;

namespace Scripts.Services.GameSettings
{
    public interface IGameSettingsService : IService
    {
        GameGlobalSettings GameGlobalSettings { get; set; }
    }
}