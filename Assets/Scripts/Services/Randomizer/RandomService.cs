using Random = UnityEngine.Random;

namespace Scripts.Services.Randomizer
{
    public class RandomService : IRandomService
    {
        public int Next(int lootMin, int lootMax)
        {
            return Random.Range(lootMin, lootMax);
        }
    }
}