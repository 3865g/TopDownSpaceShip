using Scripts.Logic;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float currentHP;
        [SerializeField] private float maxHP;

        public event Action HealthChanged;
        public GameObject TextPrefab;
        public float CurrentHP 
        { 
            get => currentHP; 
            set => currentHP = value; 
        }
        public float MaxHP 
        { 
            get => maxHP;
            set => maxHP = value; 
        }

        public bool ReturnDamage { get; set; }
        public float ReturnedDamage { get; set; }


        public void TakeDamage(float damage)
        {
            CurrentHP -= damage;

            HealthChanged?.Invoke();

            ShowText(damage.ToString(), Color.red);
        }

        public void TakeHP(float HP)
        {
            HealthChanged?.Invoke();
            ShowText(HP.ToString(), Color.green);
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