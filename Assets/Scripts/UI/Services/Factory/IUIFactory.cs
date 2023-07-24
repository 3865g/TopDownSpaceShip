using Scripts.Services;
using System.Threading.Tasks;

namespace Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        Task CreateUIRoot();
    }
}
