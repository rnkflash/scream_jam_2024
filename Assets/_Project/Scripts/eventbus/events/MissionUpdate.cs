namespace _Project.Scripts.eventbus.events
{
    public class MissionUpdate: Message
    {
        public bool trailerAttached;
        public MissionUpdate(bool trailerAttached)
        {
            this.trailerAttached = trailerAttached;
        }
    }
}