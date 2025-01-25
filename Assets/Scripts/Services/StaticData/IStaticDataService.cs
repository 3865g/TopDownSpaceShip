using Scripts.StaticData;
using Scripts.StaticData.Windows;
using Scripts.UI.Services.Windows;
using Scripts.Hero.Ability;

namespace Scripts.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        MonsterStaticData ForMonster(MonsterTypeId typeId);

        GateStaticData ForGate(GateTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowStaticData ForWindow(WindowId windowId);

        HeroStaticData ForHero(HeroTyoeId heroTypeId);


        ConfigurationAbility ForAbility(AbilityTypeId abilityTypeId);
        SecondaryAbility ForSecondaryAbility(SecondaryAbilityTypeId abilityTypeId);
        SecondaryAbility ForAttributeAbility(SecondaryAbilityTypeId abilityTypeId);
        ConfigurationDescription ForConfiguration(ConfigurationTypeId configurationTypeId);

    }
}
