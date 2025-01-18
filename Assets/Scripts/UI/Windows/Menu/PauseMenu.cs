using Scripts.Hero.Ability;
using Scripts.Infrastructure.States;
using Scripts.Services.SecondaryAbilityService;
using Scripts.UI.Services.Windows;
using Scripts.UI.Windows.Rewards;
using UnityEngine;

namespace Scripts.UI.Windows.Menu
{

    public class PauseMenu : WindowBase
    {


        private SecondaryAbility secondaryAbility;
        private ISecondaryAbilityService _secondaryAbilityService;
        private IGameStateMachine _gameStateMachine;
        private IWindowService _windowService;
        private RewardItem[] _rewardItem;
        private GameObject _player;

        private ReturnToMainMenu _returnMainMenu;
        private RestartLevel _restartLevel;


        public void Construct(IGameStateMachine gameStateMachine, ISecondaryAbilityService secondaryAbilityService)
        {
            _gameStateMachine = gameStateMachine;
            _secondaryAbilityService = secondaryAbilityService;
            _player = _secondaryAbilityService.Player;
            _returnMainMenu = GetComponentInChildren<ReturnToMainMenu>();
            _restartLevel = GetComponentInChildren<RestartLevel>();

            SendLinks();

        }

        public void SendLinks()
        {
            _returnMainMenu.Construct(_gameStateMachine);
            _restartLevel.Construct(_gameStateMachine);
        }
    }
}
