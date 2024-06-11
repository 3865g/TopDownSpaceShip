using Scripts.Services;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreatePlanetsMenu();
        void CreateShop();
        Task CreateUIRoot();
    }
}
