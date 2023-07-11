using Scripts.Data;
using Scripts.Hero;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using Scripts.Services.Input;
using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Scripts.Enemy
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public LayerMask LayerMask;

        private IInputService _inputService;
        private CharacterController _characterController;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        

        [SerializeField]
        private float Damage = 5f;
        private float radius = 20f;
        private Stats _stats;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _layerMask = LayerMask;
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                OnAttack();
                Debug.Log("Attack");
            }
        }

        public void OnAttack()
        {
            for(int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(StartPoint(), _stats.DamageRadius, _hits, _layerMask);
        }
       

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, _characterController.center.y, transform.position.z);
        }

    }
}