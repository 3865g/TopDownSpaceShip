using Scripts.Infrastructure.States;
using Scripts.Logic;
using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.Infrastructure
{
    public class GameBootstraper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;

        private Game _game;
        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}

