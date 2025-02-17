﻿using Scripts.Hero;
using System;

namespace Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State HeroState;
        public WorldData WorldData;
        public Stats HeroStats;
        public KillData KillData;
        public AbilityData AbilityProgress;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new State();
            HeroStats = new Stats();
            KillData = new KillData();
            AbilityProgress = new AbilityData();
        }

        
    }
}
