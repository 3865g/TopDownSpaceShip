using Scripts.UI.Windows;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services.PersistentProgress;
using Scripts.Services.StaticData;
using Scripts.StaticData.Windows;
using Scripts.UI.Services.Windows;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _persistantProgressService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IPersistentProgressService persistantProgressService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _persistantProgressService = persistantProgressService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticDataService.ForWindow(WindowId.Shop);
            WindowBase windowBase = Object.Instantiate(config.Prefab, _uiRoot);
            windowBase.Construct(_persistantProgressService);
        }

        public void CreateUIRoot()
        {
           _uiRoot =  _assetProvider.Instantiate(UIRootPath).transform;
        }
    }
}
