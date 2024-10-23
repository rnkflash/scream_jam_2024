using System;
using _Project.Scripts.dialog;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.interaction;
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
                    DialogSystem.Instance.Say("Отцепи прицеп на стоянке слева от меня и получи бабки.");
                    break;
                case CargoReceiver.ReceiverState.TrailerInside:

                    switch (cargoReceiver.parkingState)
                    {
                        case CargoReceiver.ParkingState.NotParked:
                            DialogSystem.Instance.Say("Ну чего ты ждешь, паркуйся блять.");
                            break;
                        case CargoReceiver.ParkingState.PartlyOccupied:
                            DialogSystem.Instance.Say(
                                "ЕКЛМН! Кто так паркуется блять",
                                "Прицеп должен стоят пправильно"
                                );
                            break;
                        case CargoReceiver.ParkingState.FullyOccupied:
                            DialogSystem.Instance.Say("Намана! А теперь отцепи груз");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    break;
                case CargoReceiver.ReceiverState.CargoReceived:
                    if (!babkiVidal)
                    {
                        DialogSystem.Instance.Say(
                            new DialogData("Молодец! вот твои бабки", npcName, GiveBabki),
                            new DialogData("А теперь едь на другую базу за новым грузом!", npcName)
                        );    
                    }
                    else
                    {
                        DialogSystem.Instance.Say(
                            new DialogData("А теперь пиздуй на другую базу за новым грузом!", npcName)
                        );
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void GiveBabki()
        {
            EventBus<AddMoney>.Pub(new AddMoney(100));
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