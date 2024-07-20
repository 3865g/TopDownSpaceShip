using Scripts.Logic;
using UnityEngine;

namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / ShieldTier1", order = 0)]
    public class ShieldTier1Ability : ActiveAbility
    {
        public GameObject shieldPrefab;
        public GameObject spawnedShield;
        public HeroHealth health;

        public override void Activate(GameObject parent)
        {
            spawnedShield = Instantiate(shieldPrefab, parent.transform);
            spawnedShield.transform.SetParent(parent.transform);
            health = parent.GetComponent<HeroHealth>();
            health.enabled = false;

        }

        public override void Deactivate(GameObject parent)
        {
            health.enabled = true;
            Destroy(spawnedShield);
        }

    }
}
