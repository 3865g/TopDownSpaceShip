using Scripts.Data;
using Scripts.Services.PersistentProgress;
using Scripts.Logic;
using System;
using UnityEngine;
using Scripts.Services.Randomizer;
using TMPro;
using System.Collections;

namespace Scripts.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {

        public event Action HealthChanged;

        public bool CanDodge;
        public int DodgeChance;

        private State _state;
        private IRandomService _randomService;
        private int _randomValue;


        public float CurrentHP

        {
            get => _state.CurrentHP;
            set
            {
                if (_state.CurrentHP != value)
                {

                    _state.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }

        }
        public float MaxHP
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        public GameObject TextPrefab { get; set; }

        public bool ReturnDamage { get; set; }
        public float ReturnedDamage { get; set; }


        public void Construct(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public void LoadProgress(PlayerProgress progress)
        {

            _state = progress.HeroState;


            HealthChanged?.Invoke();

        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (!gameObject)
            {
                progress.HeroState.CurrentHP = CurrentHP;
                // progress.HeroState.MaxHP = MaxHP;
            }


        }

        public void TakeDamage(float damage, Color color)
        {

            if (CanDodge)
            {
                CalculateRandomValue();

                if (CurrentHP <= 0)
                {
                    return;
                }
                else if (DodgeChance >= _randomValue)
                {
                    //Play Dodge Effect
                    return;
                }
                CurrentHP -= damage;


            }
            else
            {
                if (CurrentHP <= 0)
                {
                    return;
                }
                CurrentHP -= damage;
                //Animator.PlayHit();
            }

            ShowText(damage.ToString(), Color.red);



        }

        public void TakeHP(float Hp)
        {
            if (CurrentHP <= 0 || CurrentHP >= MaxHP)
            {
                return;
            }
            if (CurrentHP + Hp <= MaxHP)
            {
                CurrentHP += Hp;
            }
            else
            {
                CurrentHP = MaxHP;
            }
            ShowText(Hp.ToString(), Color.green);
        }

        public void AddedBonusMaxHP(float bonusHP)
        {
            MaxHP += bonusHP;
            CurrentHP += bonusHP;
        }

        public void UpdateDodgeChance(int dodgeChance)
        {
            DodgeChance += dodgeChance;
        }

        public void CalculateRandomValue()
        {
            _randomValue = _randomService.Next(0, 100);
        }





        private void ShowText(string textHP, Color color)
        {
            GameObject textPrefab = Instantiate(TextPrefab, gameObject.transform.position, Quaternion.identity);
            textPrefab.transform.SetParent(gameObject.transform);
            textPrefab.GetComponent<TMP_Text>().SetText(textHP);
            textPrefab.GetComponent<TMP_Text>().color = color;

            StartCoroutine(StartDestroyTimer(textPrefab));
        }
        private IEnumerator StartDestroyTimer(GameObject textPrefab)
        {

            yield return new WaitForSeconds(0.5f);
            Destroy(textPrefab);
        }

    }
}