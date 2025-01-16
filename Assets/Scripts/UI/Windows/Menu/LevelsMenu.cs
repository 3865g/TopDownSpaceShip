using Scripts.Hero.Ability;
using Scripts.Infrastructure.States;
using Scripts.Services.StaticData;

namespace Scripts.UI.Windows.Menu
{
    public class LevelsMenu : WindowBase
    {

        public AbilityManager AbilityManager;
        public IGameStateMachine GameStateMachine;
        public IStaticDataService StaticDataService;

        public void Construct(AbilityManager abilityManager, IGameStateMachine gameStateMachine, IStaticDataService staticDataService)
        {
            AbilityManager = abilityManager;
            GameStateMachine = gameStateMachine;
            StaticDataService = staticDataService;
        }
    }
}
