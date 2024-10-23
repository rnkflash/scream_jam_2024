using _Project.Scripts.interaction;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.locations
{
    public class LocationButton: MonoBehaviour, IInteractable
    {
        private string itemName = "button";
        private string itemHint = "press E to press button";
        [SerializeField] private UnityEvent unityEvent;
        
        public void Interact()
        {
            unityEvent?.Invoke();
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