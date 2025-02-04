using System;

namespace Scripts.Data
{
    [Serializable]
    public class GameGlobalSettings
    {
        public LocalSettings LocalSettings;

        public GameGlobalSettings() 

        {
            LocalSettings = new LocalSettings();
        }
    }
}
