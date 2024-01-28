using Scripts.Infrastructure.States;
using Scripts.UI.Services.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.UI.Windows.Menu
{
    public class LevelsMenu : WindowBase
    {
        //public GameStateMachine gameStateMachine;
        //private IWindowService _windowService;

        public IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
    }
}
