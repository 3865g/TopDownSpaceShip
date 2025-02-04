using System;
using Scripts.Services;
using System.Collections.Generic;
using Scripts.Infrastructure.Factory;
using Scripts.Services.SaveLoad;
using Scripts.Services.PersistentProgress;
using Scripts.Services.StaticData;
using Scripts.UI.Services.Factory;
using Scripts.Services.GameSettings;

namespace Scripts.Infrastructure.States
{
    public partial class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, Logic.LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, services.Single<IGameFactory>(),
                services.Single<IPersistentProgressService>(),
                services.Single<IGameSettingsService>(),
                services.Single<IStaticDataService>(),
                services.Single<IUIFactory>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<IGameSettingsService>(), services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }



        public void Enter<TState, TPayLoad>(TPayLoad payload) where TState : class, IPayLoadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            if (_activeState != null)
            {
                _activeState.Exit();
            }
            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
