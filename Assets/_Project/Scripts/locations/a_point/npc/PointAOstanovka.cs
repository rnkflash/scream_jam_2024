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
    public class PointAOstanovka : MonoBehaviour, IInteractable
    {
        [SerializeField] private string npcName = "Joe";
        [SerializeField] private string interactHint = "press E to talk";

        //[SerializeField] private CargoReceiver cargoReceiver;
        private bool babkiVidal;

        public void Interact()
        {
            DialogSystem.Instance.Say(
                "Here I am… An empty bus stop, the warehouse in the dark.",
                "No one around. Alright, they said I need to take over the shift, grab the truck.",
                "Simple enough. As long as they pay on time, blood isn’t something to mess around with.",
                "Haven’t signed the contract yet, but I’ve got to get in, see what’s waiting for me."
                );
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