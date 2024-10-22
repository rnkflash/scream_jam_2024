namespace _Project.Scripts.eventbus.events
{
    public class AddMoney : Message
    {
        public int amount;
        public AddMoney(int amount)
        {
            this.amount = amount;
        }
    }
}