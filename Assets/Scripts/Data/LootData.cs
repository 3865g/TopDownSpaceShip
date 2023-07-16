using System;

namespace Scripts.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public LootPieceDataDictionary LootPieceOnScene = new LootPieceDataDictionary();

        public Action ChangedValue;


        internal void Collect(Loot loot)
        {
            Collected += loot.Value;
            ChangedValue?.Invoke();
        }
    }
}
