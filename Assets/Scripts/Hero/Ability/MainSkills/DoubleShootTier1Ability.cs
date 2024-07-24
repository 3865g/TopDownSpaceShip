using Scripts.Enemy;
using Scripts.Logic;
using Scripts.Services.SaveLoad;
using Scripts.Services;
using UnityEngine;
using Scripts.Services.PersistentProgress;
using Scripts.Data;


namespace Scripts.Hero.Ability
{

    [CreateAssetMenu(fileName = "New Skill", menuName = "MainSkills / DoubleShoot", order = 0)]
    public class DoubleShootTier1Ability : PassiveAbility/*, ISavedProgress*/
    {
        public int BurstAmount;
        public float MaxHPMultiplyer;

        private HeroAttack _heroAttack;
        private IHealth _health;
        private PlayerProgress _playerProgress;

        public override void ActivatePassive(GameObject parent)
        {
            _heroAttack = parent.GetComponent<HeroAttack>();
            _heroAttack.BurstAmount = BurstAmount;

            _health = parent.GetComponent<IHealth>();


        }
        //public void UpdateMaxHP()
        //{
        //    _health.MaxHP -= _playerProgress.HeroState.MaxHP * MaxHPMultiplyer;
        //}

        //public void LoadProgress(PlayerProgress progress)
        //{
        //    _playerProgress = progress;
        //}

        //public void UpdateProgress(PlayerProgress progress)
        //{
        //}
    }
}

