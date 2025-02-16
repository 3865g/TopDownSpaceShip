using Scripts.Logic;
using UnityEngine;

namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "MainSkills / ShieldTier1", order = 0)]
    public class ShieldTier1Ability : ConfigurationAbility
    {
        public GameObject ShieldGameObject;
        public bool DamageWave;
        public int DamageWaveAmount;

        private GameObject _spawnedShield;
        private AbilityHolder _abilityHolder;

        private BoxCollider _boxCollider;
        private CharacterController _characterController;
        private bool _abilityIsActive = false;
        
        

        public override void Activate(GameObject parent)
        {
            _abilityHolder = parent.GetComponent<AbilityHolder>();

            if (_abilityHolder.CurrentAbilityState == 0)
            {
                _spawnedShield = Instantiate(ShieldGameObject, parent.transform);
                _spawnedShield.transform.SetParent(parent.transform);
                ShieldPrefab shieldPrefab = _spawnedShield.GetComponent<ShieldPrefab>();


                

                IHealth playerHealth = parent.GetComponent<IHealth>();
                shieldPrefab.Construct(playerHealth.ReturnDamage, DamageWave, playerHealth.ReturnedDamage, DamageWaveAmount, _abilityHolder);


                _boxCollider = parent.GetComponent<BoxCollider>();
                _characterController = parent.GetComponent<CharacterController>();
                _boxCollider.enabled = false;
                _characterController.detectCollisions = false;
                _abilityIsActive = true;
            }
        }

        public override void Deactivate(GameObject parent)
        {
            _abilityHolder = parent.GetComponent<AbilityHolder>();
            

            if (_abilityHolder.CurrentAbilityState == 1)
            {
                _boxCollider = parent.GetComponent<BoxCollider>();
                _characterController = parent.GetComponent<CharacterController>();
                _boxCollider.enabled = true;
                _characterController.detectCollisions = true;
                Destroy(_spawnedShield);
                _abilityIsActive = false;
            }
            
        }

    }
}
