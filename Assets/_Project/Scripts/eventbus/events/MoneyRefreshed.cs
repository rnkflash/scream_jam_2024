namespace _Project.Scripts.eventbus.events
{
    public class MoneyRefreshed : Message
    {
        public int amount;
        public MoneyRefreshed(int amount)
        {
            this.amount = amount;
        }
    }
}