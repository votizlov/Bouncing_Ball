using UnityEngine;

namespace Objects
{
    public class PlayWithOffset : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private string stateName;

        [SerializeField] private float offset;

        void Start()
        {
            Play();
        }

        public void Play()
        {
            animator.Play(stateName, 0, offset);
        }
    }
}