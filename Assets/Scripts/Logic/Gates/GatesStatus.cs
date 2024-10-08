using Scripts.Infrastructure.Factory;
using UnityEngine;

namespace Scripts.Logic.Gates
{
    public class GatesStatus : MonoBehaviour
    {
        public GameObject HitBox;
        public GameObject HpUi;

        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;

        }
        

        private void Start()
        {
            HitBox.SetActive(false);
            HpUi.SetActive(false);
        }

        public void UpdateStatus()
        {
            HitBox.SetActive(true);
            HpUi.SetActive(true);
        }
    }
}