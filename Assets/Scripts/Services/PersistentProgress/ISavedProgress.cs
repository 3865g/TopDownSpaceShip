using Scripts.Data;
using Scripts.Infrastructure.Factory;

namespace Scripts.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}
