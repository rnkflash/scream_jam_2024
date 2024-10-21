using _Project.Scripts.dialog;
using _Project.Scripts.interaction;
using UnityEngine;

namespace _Project.Scripts.npc
{
    public class PointAManager : MonoBehaviour, IInteractable
    {
        [SerializeField] private string npcName = "Joe";
        [SerializeField] private string interactHint = "press E to talk";
        
        public void Interact()
        {
            DialogSystem.Instance.Say("Отцепи прицеп на стоянке слева от меня и получи бабки.");
        }

        public string GetName()
        {
            return npcName;
        }

        public string GetHint()
        {
            return interactHint;
        }
    }
}