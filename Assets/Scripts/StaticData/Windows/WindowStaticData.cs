using Scripts.UI.Services.Windows;
using Scripts.UI.Windows;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/Window static data")]
    public class WindowStaticData : ScriptableObject
    {
        //public List<WindowConfig> Configs;

        public WindowId WindowId;
        public WindowBase Prefab;
    }
}
