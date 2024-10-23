using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.monster;
using UnityEngine;

namespace _Project.Scripts.player
{
    public class Monsters: MonoBehaviour
    {
        [SerializeField] private AfkGhoul afkGhoul;
        
        private void Start()
        {
            EventBus<AFKGhoulStartHunting>.Sub(OnAFKGhoulStartHunting);
        }

        private void OnAFKGhoulStartHunting(AFKGhoulStartHunting message)
        {
            afkGhoul.StartHunting(message.plane, 30.0f);
        }

        private void OnDestroy()
        {
            EventBus<AFKGhoulStartHunting>.Unsub(OnAFKGhoulStartHunting);
        }
    }
}