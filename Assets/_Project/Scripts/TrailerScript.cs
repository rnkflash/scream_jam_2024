using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class TrailerScript : MonoBehaviour
    {
        // Wheel Colliders
        [SerializeField] private List<WheelCollider> wheels;

        // Wheels
        [SerializeField] private List<Transform> wheelTransforms;

        private void FixedUpdate()
        {
            //UpdateWheels();
        }

        private void UpdateWheels() {
            for (int i = 0; i < wheels.Count; i++)
            {
                UpdateSingleWheel(wheels[i], wheelTransforms[i]);
            }
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
            Vector3 pos;
            Quaternion rot; 
            wheelCollider.GetWorldPose(out pos, out rot);
            if (wheelTransform != null)
            {
                wheelTransform.rotation = rot;
                wheelTransform.position = pos;    
            }
        }
    }
}
