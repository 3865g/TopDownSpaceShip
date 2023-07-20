using Scripts.Services;

namespace Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreateUIRoot();
    }
}
