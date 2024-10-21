using System;
using UnityEngine;

namespace _Project.Scripts.interaction
{
    public interface IInteractable
    {
        public void Interact();
        public String GetName();
        public String GetHint();
    }
}