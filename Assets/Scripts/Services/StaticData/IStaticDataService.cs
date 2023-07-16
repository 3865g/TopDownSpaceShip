
using Scripts.StaticData;

namespace Scripts.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void LoadMonsters();
    }
}
