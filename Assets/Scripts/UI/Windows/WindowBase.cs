using Scripts.Data;
using Scripts.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button CloseButton;

        protected IPersistentProgressService PersistantProgressService;
        protected PlayerProgress PlayerProgress => PersistantProgressService.Progress;

        public void Construct(IPersistentProgressService persistentProgressService)
        {
            PersistantProgressService = persistentProgressService;
        }

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Clenup();
        }

        protected virtual void OnAwake()
        {
            CloseButton.onClick.AddListener(() => Destroy(gameObject));


            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Clenup(){}
    }
}
