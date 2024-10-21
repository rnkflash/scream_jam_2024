using _Project.Scripts.enums;
using _Project.Scripts.interaction;
using UnityEngine;

namespace _Project.Scripts.car.interactables
{
    public class InteriorDoorLeft : MonoBehaviour, IInteractable
    {
        [SerializeField] private string itemName = "left door";
        [SerializeField] private string hint = "press E to exit";
        [SerializeField] private Transform playerExitPoint;
        
        public void Interact()
        {
            var fpsController = CameraSwitcher.Instance.GetFPSController();
            fpsController.transform.SetPositionAndRotation(playerExitPoint.position, playerExitPoint.rotation);
            CameraSwitcher.Instance.SwitchCamera(Cameras.FPS);
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
