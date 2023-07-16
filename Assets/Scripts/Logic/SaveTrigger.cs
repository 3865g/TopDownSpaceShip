using Scripts.Services;
using Scripts.Services.SaveLoad;
using UnityEngine;

namespace Scripts.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        public BoxCollider boxCollider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();

            Debug.Log("Saved");

            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!boxCollider)
            {
                return;
            }

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }
}