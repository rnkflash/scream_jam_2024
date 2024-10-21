using System;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.ui
{
    public class TargetText: MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
            
            EventBus<InteractableOnTarget>.Sub(OnTarget);
            EventBus<NoInteractableOnTarget>.Sub(OnNoTarget);
        }

        private void OnDestroy()
        {
            EventBus<InteractableOnTarget>.Unsub(OnTarget);
            EventBus<NoInteractableOnTarget>.Unsub(OnNoTarget);
        }

        private void OnTarget(InteractableOnTarget message)
        {
            text.enabled = true;
            text.text = message.interactable.GetHint();
        }

        private void OnNoTarget(NoInteractableOnTarget message)
        {
            text.enabled = false;
            text.text = "";
        }
    }
}