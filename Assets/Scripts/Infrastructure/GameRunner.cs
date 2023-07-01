using UnityEngine;


namespace Scripts.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstraper gameBootstraperPrefab;
        private void Awake()
        {
            var bootstraper = FindAnyObjectByType<GameBootstraper>();
            
            if(bootstraper == null)
            {
                Instantiate(gameBootstraperPrefab);
            }
        }
    }
}

