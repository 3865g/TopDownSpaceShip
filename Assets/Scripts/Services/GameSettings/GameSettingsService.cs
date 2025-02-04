using Scripts.Data;

namespace Scripts.Services.GameSettings
{
    public class GameSettingsService : IGameSettingsService
    {
        public GameGlobalSettings GameGlobalSettings { get; set; }
    }
}
