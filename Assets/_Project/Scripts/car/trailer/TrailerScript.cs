using System;
using System.Collections.Generic;
using _Project.Scripts.utils;
using UnityEngine;

namespace _Project.Scripts
{
    public class TrailerScript : MonoBehaviour
    {
        [SerializeField] private Transform trailerConnector;
        private HingeJoint hingeJoint;

        private void Awake()
        {
            hingeJoint = GetComponent<HingeJoint>();
        }

        public void ActivateHinge(Transform truckConnector, Rigidbody truckRigidbody)
        {
            AlignAtObject.AlignWithYourChildAt(transform, trailerConnector, truckConnector, Quaternion.identity);
            hingeJoint.connectedBody = truckRigidbody;
        }

        public void DeactivateHinge()
        {
            hingeJoint.connectedBody = null;
        }
    }
}
