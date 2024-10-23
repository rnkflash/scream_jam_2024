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
    public class PointAManager : MonoBehaviour, IInteractable
    {
        [SerializeField] private string npcName = "Joe";
        [SerializeField] private string interactHint = "press E to talk";

        [SerializeField] private CargoReceiver cargoReceiver;
        private bool babkiVidal;
        
        public void Interact()
        {
            switch (cargoReceiver.state)
            {
                case CargoReceiver.ReceiverState.Empty:
                    DialogSystem.Instance.Say("Detach the trailer at the parking lot to my left and get the cash.");
                    break;
                case CargoReceiver.ReceiverState.TrailerInside:

                    switch (cargoReceiver.parkingState)
                    {
                        case CargoReceiver.ParkingState.NotParked:
                            DialogSystem.Instance.Say("What are you waiting for? Park the damn thing.");
                            break;
                        case CargoReceiver.ParkingState.PartlyOccupied:
                            DialogSystem.Instance.Say(
                                "Who parks like that, damn it?",
                                "The trailer's got to be parked properly."
                                );
                            break;
                        case CargoReceiver.ParkingState.FullyOccupied:
                            DialogSystem.Instance.Say("Got it! Now detach the load.");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    break;
                case CargoReceiver.ReceiverState.CargoReceived:
                    if (!babkiVidal)
                    {
                        DialogSystem.Instance.Say(
                            new DialogData("Good job! Here’s your cash.", npcName, GiveBabki),
                            new DialogData("Now head to the other depot for a new load!", npcName)
                        );    
                    }
                    else
                    {
                        DialogSystem.Instance.Say(
                            new DialogData("Now get your ass to the other depot for a new load!", npcName)
                        );
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void GiveBabki()
        {
            EventBus<AddMoney>.Pub(new AddMoney(PlayerGlobal.Instance.oneMissionMoney));
            babkiVidal = true;
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