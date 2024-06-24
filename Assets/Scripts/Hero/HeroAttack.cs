using Scripts.Data;
using Scripts.Services.PersistentProgress;
using UnityEngine;
using Scripts.Hero;
using Assets.Scripts.Hero;
using System.Collections;

namespace Scripts.Enemy
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {

        public float AttackCooldown = 0.5f;
        public int BurstAmount = 1;
        //public float LaserSpeed = 500f;
        public Transform LaserStartTransform;
        public GameObject Laserprefab;

        public int shootcount;


        public bool canAttack;
        private Stats _stats;
        private float _attackCooldown;
        private RotateForAttack _roatateForAttack;

        private void Awake()
        {
            _roatateForAttack = gameObject.GetComponent<RotateForAttack>();
        }

        private void Update()
        {
            //Debug.Log(_stats.Damage);

            UpdateCooldown();


            if (ReadyAttack() && _roatateForAttack._enemy != null)
            {
                StartCoroutine(OnAttack());
            }
        }



        public IEnumerator OnAttack()
        {
            shootcount = BurstAmount;

            while (shootcount > 0)
            {

                _attackCooldown = AttackCooldown;
                GameObject laserPrefab = Instantiate(Laserprefab, LaserStartTransform.position, Quaternion.identity);
                PlayerLaser laser = laserPrefab.GetComponent<PlayerLaser>();
                Vector3 laserDirection = (_roatateForAttack._enemy.transform.position - LaserStartTransform.position).normalized;
                laser.Construct(laserDirection, _stats.Damage);

                shootcount--;

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
            return  /*_isAttacking &&*/ canAttack && CooldownIsUp();
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