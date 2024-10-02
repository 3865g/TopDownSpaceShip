using Scripts.Services;
using Scripts.UI.Services.Windows;
using Scripts.UI.Windows.Menu;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {

        void CreatePlanetsMenu();
        void CreateRewardsMenu();
        void CreateShop();
        void CreateChoiceWindow(IWindowService windowService);
        Task CreateUIRoot();
    }
}
