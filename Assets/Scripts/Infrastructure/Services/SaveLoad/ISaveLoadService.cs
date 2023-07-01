using Scripts.Data;

namespace Scripts.Infrastructure.Services.PersistentProgress.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}
