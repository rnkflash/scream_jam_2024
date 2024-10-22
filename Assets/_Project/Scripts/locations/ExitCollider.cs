using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.locations
{
    public class ExitCollider : MonoBehaviour
    {
        private Location location;
        private bool isTriggered;

        private void Start()
        {
            location = GetComponentInParent<Location>();
            if (location == null)
            {
                throw new Exception("no location in parent found");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isTriggered) return;
            var car = other.GameObject().GetComponent<CarController>();
            if (car != null)
            {
                isTriggered = true;
                location.OnExit(gameObject.name);
            }
        }
    }
}
