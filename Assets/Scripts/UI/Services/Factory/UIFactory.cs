using Scripts.Infrastructure.AssetManagement;
using Scripts.Services.PersistentProgress;
using Scripts.Services.StaticData;
using Scripts.StaticData.Windows;
using Scripts.UI.Services.Windows;
using UnityEngine;
using Scripts.Services.Ads;
using System.Threading.Tasks;
using Scripts.UI.Windows.Shop;
using Scripts.UI.Windows.Menu;
using Scripts.Infrastructure.States;
using Scripts.Services.SecondaryAbilityService;

namespace Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {

        private const string UIRootPath = "UIRoot";
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _persistantProgressService;
        private readonly IAdsService _adsService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISecondaryAbilityService _secondaryAbilityService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IPersistentProgressService persistantProgressService,
            IAdsService adsService,
            IGameStateMachine gameStateMachine,
            ISecondaryAbilityService secondaryAbilityService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _persistantProgressService = persistantProgressService;
            _adsService = adsService;
            _gameStateMachine = gameStateMachine;
            _secondaryAbilityService = secondaryAbilityService; 
        }

        public void CreatePlanetsMenu( )
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.LevelsMenu);
            LevelsMenu levlesmenu = Object.Instantiate(config.Prefab) as LevelsMenu;
            levlesmenu.Construct(_secondaryAbilityService.AbilityManager, _gameStateMachine, _staticDataService);
        }

        public void CreateRewardsMenu()
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.Rewards);
            RewardWindow rewardWindow = Object.Instantiate(config.Prefab, _uiRoot) as RewardWindow;
            rewardWindow.Construct(_secondaryAbilityService);
            rewardWindow.FillingItem();

        }

        public void CreatePauseMenu(IWindowService windowService)
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.PauseMenu);
            PauseMenu pauseMenu = Object.Instantiate(config.Prefab, _uiRoot) as PauseMenu;
            pauseMenu.Construct(_gameStateMachine, _secondaryAbilityService, windowService);
        }

        public void CreateDetailedViewAbility(IWindowService windowService)
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.DetailedViewAbilities);
            DetailedViewAbility detailedViewAbility = Object.Instantiate(config.Prefab, _uiRoot) as DetailedViewAbility;
            windowService.DetailedViewAbility = detailedViewAbility;
        }

        public void CreateShop()
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.Shop);
            ShopWindow shopWondow = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            shopWondow.Construct(_adsService, _persistantProgressService);
        }

        public void CreateChoiceWindow(IWindowService windowService)
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.ChoiceWindow);
            ChoiceWindow choiceWindow = Object.Instantiate(config.Prefab, _uiRoot) as ChoiceWindow;
            windowService.ChoiceWindow = choiceWindow;


        }
        public async Task CreateUIRoot()
        {
            GameObject root = await _assetProvider.Instantiate(UIRootPath);
            _uiRoot = root.transform;
        }

        
    }
}
