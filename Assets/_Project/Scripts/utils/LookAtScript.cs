using System;
using UnityEngine;

namespace _Project.Scripts.utils
{
    [RequireComponent(typeof(Animator))]
    public class LookAtScript : MonoBehaviour
    {
        private Transform objectToLookAt;
        public float headWeight;
        public float bodyWeight;
        private Animator animator;

        private Camera currentCamera;

        void Start()
        {
            animator = GetComponent<Animator>();
            currentCamera = Camera.main;
        }

        private void Update()
        {
            if (currentCamera != Camera.main)
            {
                currentCamera = Camera.main;
                objectToLookAt = currentCamera?.transform;
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (objectToLookAt == null) return;
            animator.SetLookAtPosition(objectToLookAt.position);
            animator.SetLookAtWeight(1, bodyWeight, headWeight);
        }
    }
}