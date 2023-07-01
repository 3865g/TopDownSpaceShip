using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("Die");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayDeath()
        {
            _animator.CrossFade(Die, 0.5f);
        }
    }
}