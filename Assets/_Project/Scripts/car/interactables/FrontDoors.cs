using _Project.Scripts.enums;
using _Project.Scripts.interaction;
using UnityEngine;

namespace _Project.Scripts.car.interactables
{
    public class FrontDoors : MonoBehaviour, IInteractable
    {
        [SerializeField] private string itemName = "front door";
        [SerializeField] private string hint = "press E to enter";
        
        public void Interact()
        {
            CameraSwitcher.Instance.SwitchCamera(Cameras.TruckInterior);
        }

        public string GetName()
        {
            return itemName;
        }

        public string GetHint()
        {
            return hint;
        }
    }
}
