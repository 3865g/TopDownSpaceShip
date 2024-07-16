using Scripts.Hero.Ability;
using Scripts.Infrastructure.States;

namespace Scripts.UI.Windows.Menu
{
    public class LevelsMenu : WindowBase
    {

        public AbilityManager AbilityManager;
        public IGameStateMachine GameStateMachine;

        public void Construct(AbilityManager abilityManager, IGameStateMachine gameStateMachine)
        {
            AbilityManager = abilityManager;
            GameStateMachine = gameStateMachine;
        }
    }
}
