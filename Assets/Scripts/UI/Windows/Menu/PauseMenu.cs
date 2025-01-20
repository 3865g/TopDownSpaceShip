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
        private DetailsContent _detailsContent;


        public void Construct(IGameStateMachine gameStateMachine, ISecondaryAbilityService secondaryAbilityService, IWindowService windowService)
        {
            _gameStateMachine = gameStateMachine;
            _secondaryAbilityService = secondaryAbilityService;
            _player = _secondaryAbilityService.Player;
            _windowService = windowService;
            _returnMainMenu = GetComponentInChildren<ReturnToMainMenu>();
            _restartLevel = GetComponentInChildren<RestartLevel>();
            _detailsContent = GetComponentInChildren<DetailsContent>();


            SendLinks();

        }

        public void SendLinks()
        {
            if (_returnMainMenu)
            {
                _returnMainMenu.Construct(_gameStateMachine);
            }
            if (_restartLevel)
            {
                _restartLevel.Construct(_gameStateMachine);
            }
            if (_detailsContent)
            {
                _detailsContent.Construct(_windowService, _player);
            }
        }
    }
}
