using Scripts.Services;
using Scripts.UI.Windows.Menu;
using UnityEngine;

namespace Scripts.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);

        //Need here???
        ChoiceWindow ChoiceWindow { get; set; }
    }
}