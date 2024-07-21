using Scripts.Logic;
using UnityEngine;

namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / ShieldTier1", order = 0)]
    public class ShieldTier1Ability : ActiveAbility
    {
        public GameObject shieldPrefab;
        public GameObject spawnedShield;

        private BoxCollider _boxCollider;
        private CharacterController _characterController;

        public override void Activate(GameObject parent)
        {
            spawnedShield = Instantiate(shieldPrefab, parent.transform);
            spawnedShield.transform.SetParent(parent.transform);

            IHealth playerHealth = parent.GetComponent<IHealth>();
            spawnedShield.GetComponent<ShieldPrefab>().Construct(playerHealth.ReturnDamage, playerHealth.ReturnedDamage);


            _boxCollider = parent.GetComponent<BoxCollider>();
            _boxCollider.enabled = false;
            _characterController = parent.GetComponent<CharacterController>();
            _characterController.detectCollisions = false;

        }

        public override void Deactivate(GameObject parent)
        {
            _boxCollider.enabled = true;
            _characterController.detectCollisions = true;
            Destroy(spawnedShield);
        }

    }
}
