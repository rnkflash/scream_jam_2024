using _Project.Scripts.dialog;
using _Project.Scripts.interaction;
using UnityEngine;

namespace _Project.Scripts.locations.b_point.npc
{
    public class PointBManager : MonoBehaviour, IInteractable
    {
        [SerializeField] private string npcName = "Joe";
        [SerializeField] private string interactHint = "press E to talk";
        
        public void Interact()
        {
            DialogSystem.Instance.Say("Grab any trailer and deliver it to the next depot; you’ll get a lot of cash.");
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