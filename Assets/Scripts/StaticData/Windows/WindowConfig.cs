
using Scripts.UI.Windows;
using Scripts.UI.Services.Windows;
using System;

namespace Scripts.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}
