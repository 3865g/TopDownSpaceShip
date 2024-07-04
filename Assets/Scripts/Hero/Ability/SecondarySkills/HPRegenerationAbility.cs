using Scripts.Logic;
using UnityEngine;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System / HPRegenerationAbility", order = 0)]
    public class HPRegenerationAbility : SecondaryAbility
    {
        public int RegenerationAmount;
        public int RegenerationInterval;
        public GameObject RegenerationPrefab;

        private GameObject _regenerationPrefab;
        private HPRegeneration _hpRegeneration;

        public override void ActivatePassive(GameObject parent)
        {
            _regenerationPrefab = Instantiate(RegenerationPrefab, parent.transform);
            _regenerationPrefab.transform.SetParent(parent.transform);

            _hpRegeneration = _regenerationPrefab.GetComponent<HPRegeneration>();
            _hpRegeneration.Construct(parent);
            _hpRegeneration.RegenerationAmount = RegenerationAmount;
            _hpRegeneration.RegenerationInterval = RegenerationInterval;


        }
    }
}

