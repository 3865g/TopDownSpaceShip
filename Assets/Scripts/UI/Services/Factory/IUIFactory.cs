using Scripts.Services;
using Scripts.UI.Services.Windows;
using System.Threading.Tasks;

namespace Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {

        void CreatePlanetsMenu();
        void CreateRewardsMenu();
        void CreatePauseMenu();
        void CreateShop();
        void CreateChoiceWindow(IWindowService windowService);
        Task CreateUIRoot();
    }
}
