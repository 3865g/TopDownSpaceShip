using Scripts.Logic;
using UnityEngine;

namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "MainSkills / ShieldTier1", order = 0)]
    public class ShieldTier1Ability : ActiveAbility
    {
        public GameObject ShieldGameObject;
        public GameObject SpawnedShield;
        public bool DamageWave;
        public int DamageWaveAmount;

        private BoxCollider _boxCollider;
        private CharacterController _characterController;
        private bool _abilityIsActive;

        public override void Activate(GameObject parent)
        {
            if (!_abilityIsActive)
            {
                SpawnedShield = Instantiate(ShieldGameObject, parent.transform);
                SpawnedShield.transform.SetParent(parent.transform);
                ShieldPrefab shieldPrefab = SpawnedShield.GetComponent<ShieldPrefab>();

                IHealth playerHealth = parent.GetComponent<IHealth>();
                shieldPrefab.Construct(playerHealth.ReturnDamage, DamageWave, playerHealth.ReturnedDamage, DamageWaveAmount);


                _boxCollider = parent.GetComponent<BoxCollider>();
                _boxCollider.enabled = false;
                _characterController = parent.GetComponent<CharacterController>();
                _characterController.detectCollisions = false;
                _abilityIsActive = true;
            }

        }

        public override void Deactivate(GameObject parent)
        {
            if (_abilityIsActive)
            {
                _boxCollider.enabled = true;
                _characterController.detectCollisions = true;
                Destroy(SpawnedShield);
                _abilityIsActive = false;
            }
        }

    }
}
