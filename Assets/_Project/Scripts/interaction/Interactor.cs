using System;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using UnityEngine;

namespace _Project.Scripts.interaction
{
    public class Interactor: MonoBehaviour
    {
        [SerializeField]private Transform interactionSource;
        [SerializeField] private float interactionRange = 2.0f;

        private IInteractable currentlyLookingAt;
        
        private void Start()
        {
            
        }

        private void OnEnable()
        {
            currentlyLookingAt = null;
        }

        private void Update()
        {
            var interactable = RayCast();
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable?.Interact();
            }

            if (interactable == null)
            {
                if (currentlyLookingAt != null)
                    OnStopLooking();
            }
            else
            {
                if (currentlyLookingAt != interactable)
                {
                    if (currentlyLookingAt != null)
                        OnStopLooking();
                    OnStartLooking(interactable);
                }    
            }
        }

        private void OnStartLooking(IInteractable interactable)
        {
            currentlyLookingAt = interactable;
            EventBus<InteractableOnTarget>.Pub(new InteractableOnTarget(interactable));
        }
        
        private void OnStopLooking()
        {
            currentlyLookingAt = null;
            EventBus<NoInteractableOnTarget>.Pub(new NoInteractableOnTarget());
        }

        private IInteractable RayCast()
        {
            Ray r = new Ray(interactionSource.position, interactionSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactionRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    return interactable;
                } 
            }

            return null;
        }
    }
}