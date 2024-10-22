using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.locations.a_point
{
    public class CargoReceiver : MonoBehaviour
    {
        public enum ReceiverState
        {
            Empty, TrailerInside, CargoReceived
        }
        
        public enum ParkingState
        {
            NotParked, PartlyOccupied, FullyOccupied
        }

        [HideInInspector] public ReceiverState state = ReceiverState.Empty;
        [HideInInspector] public ParkingState parkingState = ParkingState.NotParked;
        private TrailerScript trailer;
        
        private Collider collider;

        private void Awake()
        {
            collider = GetComponent<Collider>();
        }

        private void Update()
        {
            if (state == ReceiverState.CargoReceived) return;
            
            if (state == ReceiverState.TrailerInside)
            {
                CheckParkingState();
                
                if (!trailer.IsAttached() && parkingState == ParkingState.FullyOccupied)
                {
                    ReceiveCargo();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (state == ReceiverState.CargoReceived) return;
            
            var trailer = other.gameObject.GetComponentInParent<TrailerScript>();
            if (!trailer) return;

            state = ReceiverState.TrailerInside;
            this.trailer = trailer;
        }

        private void OnTriggerExit(Collider other)
        {
            if (state == ReceiverState.CargoReceived) return;
            
            var trailer = other.gameObject.GetComponentInParent<TrailerScript>();
            if (!trailer) return;
            
            state = ReceiverState.Empty;
            this.trailer = null;
        }

        private void CheckParkingState()
        {
            if (state == ReceiverState.CargoReceived) return;
            List<Transform> trailerReceiverPorts = trailer.GetReceiverPorts();

            var parkedPorts = trailerReceiverPorts.Count(transform1 => collider.bounds.Contains(transform1.position));

            if (parkedPorts > 0)
            {
                if (parkedPorts == trailerReceiverPorts.Count)
                {
                    //fully parked
                    parkingState = ParkingState.FullyOccupied;
                    
                }
                else
                {
                    //partly parked
                    parkingState = ParkingState.PartlyOccupied;
                }    
            }
            else
            {
                parkingState = ParkingState.NotParked;
            }
        }

        private void ReceiveCargo()
        {
            state = ReceiverState.CargoReceived;
            trailer.transform.SetParent(transform);
            trailer.enabled = false;
            
            EventBus<CargoReceived>.Pub(new CargoReceived());
        }
    }
}
