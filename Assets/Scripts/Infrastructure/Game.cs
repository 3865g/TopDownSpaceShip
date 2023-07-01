using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.States;

namespace Scripts.Infrastructure
{
    public class Game
    {

        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, Logic.LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }

        
    }
}

