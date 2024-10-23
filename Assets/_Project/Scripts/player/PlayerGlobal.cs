using System;
using System.Collections.Generic;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.jumpscares;
using _Project.Scripts.locations;
using _Project.Scripts.scriptables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.player
{
    public class PlayerGlobal: utils.Singleton<PlayerGlobal>
    {
        public int startingMoney = 0;
        public int goalMoney = 60000;
        public int oneMissionMoney = 10000;

        public int money = 0;

        public bool isOnMission;
        
        private AudioSource audioSource;
        [SerializeField] private AudioClip moneyKaChing;
        [SerializeField] private AFKGhoulJumpScare afkGhoulJumpScare;

        private void Awake()
        {
            money = startingMoney;
            audioSource = GetComponent<AudioSource>();
            EventBus<AddMoney>.Sub(OnAddMoney);
            EventBus<MissionUpdate>.Sub(OnMissionUpdate);
            EventBus<JumpScareEvent>.Sub(OnJumpScare);
            
        }

        private void OnJumpScare(JumpScareEvent message)
        {
            afkGhoulJumpScare.Scare(() =>
            {
                Debug.Log("you are dead lol");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }

        public void PlaySound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        protected virtual void OnDestroy()
        {
            EventBus<AddMoney>.Unsub(OnAddMoney);
            EventBus<MissionUpdate>.Unsub(OnMissionUpdate);
            EventBus<JumpScareEvent>.Unsub(OnJumpScare);
        }

        private void OnMissionUpdate(MissionUpdate message)
        {
            isOnMission = message.trailerAttached;
        }

        private void OnAddMoney(AddMoney message)
        {
            money += message.amount;
            EventBus<MoneyRefreshed>.Pub(new MoneyRefreshed(money));
            
            audioSource.PlayOneShot(moneyKaChing);
        }

        public AFKGhoulJumpScare GetJumpScare()
        {
            return afkGhoulJumpScare;
        }
    }
}