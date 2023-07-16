using Scripts.Data;
using Scripts.Infrastructure.Factory;
using Scripts.Services.Randomizer;
using UnityEngine;

namespace Scripts.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _gameFactory;
        private IRandomService _randomService;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory gameFactory, IRandomService randomService)
        {
            _gameFactory = gameFactory;
            _randomService = randomService;
        }

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        private void SpawnLoot()
        {
            EnemyDeath.Happened -= SpawnLoot;

            LootPiece lootPiece = _gameFactory.CreateLoot();
            lootPiece.transform.position = transform.position;

            Loot lootItem = GenerateLoot();

            lootPiece.Initialize(lootItem);
        }

        private Loot GenerateLoot()
        {
            return new Loot
            {
                Value = _randomService.Next(_lootMin, _lootMax)
            };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}