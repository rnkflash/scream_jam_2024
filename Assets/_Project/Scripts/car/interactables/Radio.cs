using System;
using _Project.Scripts.interaction;
using _Project.Scripts.radio_station;
using UnityEngine;

namespace _Project.Scripts.car.interactables
{
    public class Radio: MonoBehaviour, IInteractable
    {
        [SerializeField] private string itemName = "radio";
        [SerializeField] private string itemHint = "press E to switch radio";
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject[] radioLights;

        private void Start()
        {
            RadioStation.Instance.SetAudioSource(audioSource);
            
            foreach (var radioLight in radioLights)
            {
                radioLight.SetActive(false);
            }
        }

        public void Interact()
        {
            var radioOn = RadioStation.Instance.NextStation();
            foreach (var radioLight in radioLights)
            {
                radioLight.SetActive(radioOn);
            }
        }

        public string GetName()
        {
            return itemName;
        }

        public string GetHint()
        {
            return itemHint;
        }
    }
}