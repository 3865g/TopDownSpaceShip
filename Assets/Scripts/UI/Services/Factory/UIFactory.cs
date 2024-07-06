using Scripts.UI.Windows;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services.PersistentProgress;
using Scripts.Services.StaticData;
using Scripts.StaticData.Windows;
using Scripts.UI.Services.Windows;
using UnityEngine;
using CodeBase.Services.Ads;
using System.Threading.Tasks;
using Scripts.UI.Windows.Shop;
using Scripts.UI.Windows.Menu;
using Scripts.Infrastructure.States;
using Scripts.Infrastructure.Factory;

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

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IPersistentProgressService persistantProgressService,
            IAdsService adsService,
            IGameStateMachine gameStateMachine)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _persistantProgressService = persistantProgressService;
            _adsService = adsService;
            _gameStateMachine = gameStateMachine;
        }

        public void CreatePlanetsMenu( )
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.LevelsMenu);
            LevelsMenu levlesmenu = Object.Instantiate(config.Prefab) as LevelsMenu;
            levlesmenu.Construct(_gameStateMachine);
        }

        public void CreateRewardsMenu()
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.Rewards);
            ShopWindow shopWondow = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;

        }

        public void CreateShop()
        {
            WindowStaticData config = _staticDataService.ForWindow(WindowId.Shop);
            ShopWindow shopWondow = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            shopWondow.Construct(_adsService, _persistantProgressService);
        }
        public async Task CreateUIRoot()
        {
            GameObject root = await _assetProvider.Instantiate(UIRootPath);
            _uiRoot = root.transform;
        }

        
    }
}
