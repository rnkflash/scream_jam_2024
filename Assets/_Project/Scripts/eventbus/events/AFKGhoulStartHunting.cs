using UnityEngine;

namespace _Project.Scripts.eventbus.events
{
    public class AFKGhoulStartHunting : Message
    {
        public GameObject plane;
        public AFKGhoulStartHunting(GameObject plane)
        {
            this.plane = plane;
        }
    }
}