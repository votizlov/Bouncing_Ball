using System;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class Ball : MonoBehaviour
    {
        public UnityEvent OnCollidedWithEnemy;
        public UnityEvent OnBounce;

        [SerializeField] private Animator animator;
        private int currentState = 1;

        public void BounceLeft()
        {
            if (currentState - 1 > -1)
            {
                SetAnimTrigger(--currentState);
            }
        }

        public void BounceRight()
        {
            if (currentState + 1 < 3)
            {
                SetAnimTrigger(++currentState);
            }
        }

        public void CountBounce()
        {
            OnBounce?.Invoke();
        }

        private void SetAnimTrigger(int id)
        {
            switch (id)
            {
                case 0: animator.SetTrigger("bounceLeft");
                    break;
                case 1: animator.SetTrigger("bounceCenter");
                    break;
                case 2: animator.SetTrigger("bounceRight");
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("enemy"))
            {
                OnCollidedWithEnemy?.Invoke();
            }
        }
    }
}