using System;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.ui
{
    public class TargetCursor: MonoBehaviour
    {
        private Image crosshair;
        [SerializeField] private Color selectedColor = Color.red;
        [SerializeField] private Color defaultColor = Color.white;

        private void Awake()
        {
            crosshair = GetComponent<Image>();
            
            EventBus<InteractableOnTarget>.Sub(OnTarget);
            EventBus<NoInteractableOnTarget>.Sub(OnNoTarget);
        }

        protected virtual void OnDestroy()
        {
            EventBus<InteractableOnTarget>.Unsub(OnTarget);
            EventBus<NoInteractableOnTarget>.Unsub(OnNoTarget);
        }

        private void OnTarget(InteractableOnTarget message)
        {
            crosshair.color = selectedColor;
        }
        
        private void OnNoTarget(NoInteractableOnTarget message)
        {
            crosshair.color = defaultColor;
        }
    }
}