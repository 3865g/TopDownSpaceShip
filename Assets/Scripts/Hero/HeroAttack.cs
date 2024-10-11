using Scripts.Data;
using Scripts.Services.PersistentProgress;
using UnityEngine;
using Scripts.Hero;
using Assets.Scripts.Hero;
using System.Collections;
using Scripts.Services.Randomizer;
using Scripts.Weapon;
using System;

namespace Scripts.Enemy
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {

        public float AttackCooldown = 1.25f;
        public int BonuseDamage;
        public Action Shooting;

        public int BurstAmount;
        //public float LaserSpeed = 500f;
        public Transform LaserStartTransform;
        public GameObject Laserprefab;
        public Color CurrentColor;
        public Color DefaultColor = new Color(0, 255, 255, 255);

        public int shootcount;
        public bool CanCrit;
        public float CriticalChance;
        public float CriticalDamage;


        public bool canAttack;
        private float _attackCooldown;
        private float _criticalDamge;
        private int _randomValue;
        private Stats _stats;
        private RotateForAttack _roatateForAttack;
        private IRandomService _randomService;

        private Vector3 laserDirection;

        public void Construct(IRandomService randomService)
        {
            _randomService = randomService;
        }

        private void Awake()
        {
            _roatateForAttack = gameObject.GetComponent<RotateForAttack>();
        }

        private void Update()
        {
            //Debug.Log(_stats.Damage);

            UpdateCooldown();


            if (ReadyAttack())
            {
                StartCoroutine(OnAttack());
            }
        }

        public void UptadeBonuseDamage(int addedDamage)
        {
            BonuseDamage += addedDamage;
        }

        public void CalculateCriticalChance()
        {
            _randomValue = _randomService.Next(0, 100);

           // Debug.Log(_randomValue);

            if(CriticalChance >= _randomValue)
            {
                _criticalDamge = CriticalDamage;
                CurrentColor = new Color(255, 104, 0, 255);
            }
            else
            {
                _criticalDamge = 0;
                CurrentColor = DefaultColor;
            }
        }

        public void UpdateAtackCooloduwn(float changedCooldown)
        {
            AttackCooldown -= changedCooldown;
        }

        public IEnumerator OnAttack()
        {
            if (BurstAmount == 0)
            {
                BurstAmount = 1;
            }

            shootcount = BurstAmount;

            while (shootcount > 0)
            {
                CalculateCriticalChance();

                _attackCooldown = AttackCooldown;

                if (_roatateForAttack._enemy)
                {
                     laserDirection = (_roatateForAttack._enemy.transform.position - LaserStartTransform.position).normalized;
                }
                else
                {
                     laserDirection = gameObject.transform.forward;
                }

                GameObject laserPrefab = Instantiate(Laserprefab, LaserStartTransform.position, Quaternion.identity);
                IProjectile laser = laserPrefab.GetComponent<IProjectile>();
                laser.Construct(laserDirection, _stats.Damage + BonuseDamage + _criticalDamge, CurrentColor);

                //if (laserDirection != null)
                //{
                //    GameObject laserPrefab = Instantiate(Laserprefab, LaserStartTransform.position, Quaternion.identity);
                //    IProjectile laser = laserPrefab.GetComponent<IProjectile>();
                //    laser.Construct(laserDirection, _stats.Damage + BonuseDamage + _criticalDamge, CurrentColor);
                //}

                shootcount--;
                Shooting?.Invoke();

                yield return new WaitForSeconds(0.1f);


            }

            //Debug.Log(_attackCooldown);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }



        private bool ReadyAttack()
        {
            return  /*_isAttacking &&*/ canAttack && CooldownIsUp() /* && _roatateForAttack._enemy != null */;
        }



        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _attackCooldown -= Time.deltaTime;
            }
        }
        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0f;
        }

    }
}