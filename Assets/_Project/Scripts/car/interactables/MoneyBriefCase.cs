using System;
using _Project.Scripts.dialog;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.interaction;
using _Project.Scripts.player;
using Doublsb.Dialog;
using UnityEngine;

namespace _Project.Scripts.car.interactables
{
    public class MoneyBriefCase: MonoBehaviour, IInteractable
    {
        [SerializeField] private string itemName = "money";
        [SerializeField] private string itemHint = "press E to count money";
        [SerializeField] private GameObject[] moneyKu4ka;
        [SerializeField] private int eachMoneyKu4kaAmount = 10000;

        private void Start()
        {
            EventBus<MoneyRefreshed>.Sub(OnMoneyRefreshed);
            UpdateMoneyKu4ka(PlayerGlobal.Instance.money);
        }

        private void OnDestroy()
        {
            EventBus<MoneyRefreshed>.Unsub(OnMoneyRefreshed);
        }

        private void OnMoneyRefreshed(MoneyRefreshed message)
        {
            UpdateMoneyKu4ka(message.amount);
        }

        private void UpdateMoneyKu4ka(int amount)
        {
            var howMany = (int)Math.Ceiling(Convert.ToDouble(amount) / Convert.ToDouble(eachMoneyKu4kaAmount));
            for (int i = 0; i < moneyKu4ka.Length; i++)
            {
                moneyKu4ka[i].SetActive(false);
            }
            
            for (int i = 0; i < Math.Min(howMany, moneyKu4ka.Length); i++)
            {
                moneyKu4ka[i].SetActive(true);
            }
        }

        public void Interact()
        {
            if (PlayerGlobal.Instance.money <= 0)
            {
                DialogSystem.Instance.Say(
                    new DialogData("Деняк у меня нифига нет", itemName),
                    new DialogData("а должен я им около " + PlayerGlobal.Instance.goalMoney + "$."),
                    new DialogData("Эх.....")
                );
            }
            else
            {
                DialogSystem.Instance.Say(
                    new DialogData("Деняк у меня всего " + PlayerGlobal.Instance.money + " $", itemName),
                    new DialogData("а должен я им еще " + (PlayerGlobal.Instance.goalMoney - PlayerGlobal.Instance.money) + "$.")
                );
            }
            
        }

        public string GetName()
        {
            return itemName;
        }

        public string GetHint()
        {
            return itemHint;
        }
    }
}