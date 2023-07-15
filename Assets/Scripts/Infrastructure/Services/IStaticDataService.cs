using Scripts.Infrastructure.Services;

namespace Scripts.StaticData
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void LoadMonsters();
    }
}
