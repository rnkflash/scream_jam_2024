using _Project.Scripts.interaction;
using UnityEngine;

namespace _Project.Scripts.car.interactables
{
    public class Radio: MonoBehaviour, IInteractable
    {
        [SerializeField] private string itemName = "radio";
        [SerializeField] private string itemHint = "press E to switch radio";
        
        public void Interact()
        {
            Debug.Log("switching radio channel...");
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