using Assets.Scripts.UI.Elements;
using Scripts.Infrastructure.States;
using Scripts.UI.Services.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.UI.Windows.Menu
{
    public class LevelsMenu : WindowBase
    {

        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            foreach (ChangeLevelButton changeLevelButton in GetComponentsInChildren<ChangeLevelButton>())
            {
                changeLevelButton.Construct(_gameStateMachine);
            }
        }
    }
}
