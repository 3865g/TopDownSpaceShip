using Scripts.StaticData;
using Scripts.StaticData.Windows;
using Scripts.UI.Services.Windows;

namespace Scripts.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowId windowId);
    }
}
