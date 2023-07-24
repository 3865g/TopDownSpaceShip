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

namespace Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UIRoot";
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _persistantProgressService;
        private readonly IAdsService _adsService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IPersistentProgressService persistantProgressService, IAdsService adsService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _persistantProgressService = persistantProgressService;
            _adsService = adsService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticDataService.ForWindow(WindowId.Shop);
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
