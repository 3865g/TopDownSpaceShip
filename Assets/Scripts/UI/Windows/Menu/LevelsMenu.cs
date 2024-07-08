using Assets.Scripts.UI.Elements;
using Scripts.Infrastructure.Factory;
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

        public IGameStateMachine _gameStateMachine;
        public GameObject _abilityManager;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
    }
}
