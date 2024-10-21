using _Project.Scripts.interaction;

namespace _Project.Scripts.eventbus.events
{
    public class InteractableOnTarget : Message
    {
        public IInteractable interactable;
        public InteractableOnTarget(IInteractable target)
        {
            interactable = target;
        }
    }
    
    public class NoInteractableOnTarget : Message
    {
        public NoInteractableOnTarget()
        {
        }
    }
}