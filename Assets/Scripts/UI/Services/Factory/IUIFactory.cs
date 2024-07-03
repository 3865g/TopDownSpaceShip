using Scripts.Services;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreatePlanetsMenu();
        void CreateRewardsMenu();
        void CreateShop();
        Task CreateUIRoot();
    }
}
