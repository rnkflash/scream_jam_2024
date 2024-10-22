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
            if (Input.GetMouseButtonDown(0))
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