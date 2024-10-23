using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.scriptables;
using UnityEngine;

namespace _Project.Scripts.player
{
    public class PlayerGlobal: utils.Singleton<PlayerGlobal>
    {
        public int startingMoney = 0;
        public int goalMoney = 60000;
        public int oneMissionMoney = 10000;

        public int money = 0;

        private AudioSource audioSource;
        [SerializeField] private AudioClip moneyKaChing;
        

        private void Start()
        {
            money = startingMoney;
            EventBus<AddMoney>.Sub(OnAddMoney);
            audioSource = GetComponent<AudioSource>();
        }

        protected virtual void OnDestroy()
        {
            EventBus<AddMoney>.Unsub(OnAddMoney);
        }

        private void OnAddMoney(AddMoney message)
        {
            money += message.amount;
            EventBus<MoneyRefreshed>.Pub(new MoneyRefreshed(money));
            
            audioSource.PlayOneShot(moneyKaChing);
        }
    }
}