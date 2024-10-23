using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.locations.kpp
{
    public class VOROTA: MonoBehaviour
    {
        [SerializeField] private Transform openPosition;
        [SerializeField] private Transform closedPosition;
        [SerializeField] private Transform doorTransform;
        [SerializeField] private float openingDuration = 10.0f;
        [SerializeField] private AudioClip doorOpeningSound;
        private AudioSource audioSource;
        private bool isOpening;
        private bool isOpen;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.Stop();
        }

        [Button]
        public void Open()
        {
            if (isOpening) return;
            
            isOpening = true;
            
            audioSource.clip = doorOpeningSound;
            audioSource.loop = true;
            audioSource.Play();
            
            doorTransform.DOMove(openPosition.position, openingDuration, true).OnComplete(() =>
            {
                audioSource.Stop();
                isOpen = true;
                isOpening = false;
            });
        }
        
        [Button]
        public void Close()
        {
            if (isOpening) return;
            
            isOpening = true;
            
            audioSource.clip = doorOpeningSound;
            audioSource.loop = true;
            audioSource.Play();
            
            doorTransform.DOMove(closedPosition.position, openingDuration, true).OnComplete(() =>
            {
                audioSource.Stop();
                isOpen = false;
                isOpening = false;
            });
        }
        
    }
}