using System;
using _Project.Scripts.dialog;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.interaction;
using _Project.Scripts.player;
using Doublsb.Dialog;
using UnityEngine;

namespace _Project.Scripts.locations.a_point.npc
{
    public class PointAGuard : MonoBehaviour, IInteractable
    {
        [SerializeField] private string npcName = "Joe";
        [SerializeField] private string interactHint = "press E to talk";

        //[SerializeField] private CargoReceiver cargoReceiver;
        private bool babkiVidal;
        
        public void Interact()
        {
            DialogSystem.Instance.Say("Ah, there you are. Just got here? Alright, head right, go to the end. The manager’s there. He’ll fill you in. And hurry — everyone's tense from the delays.");
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