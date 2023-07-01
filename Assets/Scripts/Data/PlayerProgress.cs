using System;

namespace Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string initialLvl)
        {
            WorldData = new WorldData(initialLvl); 
        }        
    }
}
