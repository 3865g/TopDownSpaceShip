using Scripts.UI.Services.Factory;
using System.Diagnostics;

namespace Scripts.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;
                case WindowId.LevelsMenu:
                    _uiFactory.CreatePlanetsMenu();
                    break;
            }

        }
    }
}
