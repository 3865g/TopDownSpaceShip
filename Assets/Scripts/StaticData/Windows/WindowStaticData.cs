using System.Collections.Generic;
using UnityEngine;

namespace Scripts.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/Window static data")]
    internal class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}
