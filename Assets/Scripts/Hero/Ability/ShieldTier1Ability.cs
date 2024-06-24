using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / ShieldTier1", order = 0)]
    public class ShieldTier1Ability : ActiveAbility
    {
        public GameObject shieldPrefab;
        public GameObject spawnedShield;

        public override void Activate(GameObject parent)
        {
            spawnedShield = Instantiate(shieldPrefab, parent.transform);
            spawnedShield.transform.SetParent(parent.transform);
        }

        public override void Deactivate(GameObject parent)
        {
            Destroy(spawnedShield);
        }

    }
}
