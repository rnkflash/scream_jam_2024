namespace _Project.Scripts.eventbus.events
{
    public class AFKTimerEvent : Message
    {
        public bool afk;

        public AFKTimerEvent(bool afk)
        {
            this.afk = afk;
        }
    }
}