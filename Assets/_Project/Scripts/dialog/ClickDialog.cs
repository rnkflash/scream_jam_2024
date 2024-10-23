using System;
using Doublsb.Dialog;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.dialog
{
    public class ClickDialog: MonoBehaviour
    {
        [SerializeField] private DialogManager dialogManager;

        private void Update()
        {
            if (dialogManager.state == State.Deactivate)
                return;
            
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                OnClick();    
            }
            
            
        }

        private void OnClick()
        {
            dialogManager.Click_Window();
        }
    }
}