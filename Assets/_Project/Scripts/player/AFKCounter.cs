using System;
using System.Collections;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using UnityEngine;

namespace _Project.Scripts.player
{
    public class AFKCounter: MonoBehaviour
    {
        [SerializeField] private float afkTime = 30.0f;
        [SerializeField] private GameObject huntingGround;

        private void Start()
        {
            EventBus<AFKTimerEvent>.Sub(OnAfkEvent);
        }

        private void OnDestroy()
        {
            EventBus<AFKTimerEvent>.Unsub(OnAfkEvent);
        }

        private void OnAfkEvent(AFKTimerEvent message)
        {
            if (message.afk)
                StartCounting();
            else
                StopCounting();
        }

        public void StartCounting()
        {
            StopAllCoroutines();
            Debug.Log("start counting");
            StartCoroutine(DisappearTimer(afkTime));
        }
        
        public void StopCounting()
        {
            Debug.Log("stop counting");
            StopAllCoroutines();
            EventBus<AFKGhoulStopHunting>.Pub(new AFKGhoulStopHunting());
        }

        private void VreamyaPrishlo()
        {
            Debug.Log("VreamyaPrishlo");
            EventBus<AFKGhoulStartHunting>.Pub(new AFKGhoulStartHunting(huntingGround));
        }
        
        private IEnumerator DisappearTimer(float time)
        {
            yield return new WaitForSeconds(time);
            VreamyaPrishlo();
        }

    }
}