using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class CarController : MonoBehaviour
    {
        public float currentSteerAngle { get; private set; }
        private float currentbreakForce;
        
        [HideInInspector] public float horizontalInput, verticalInput;
        [HideInInspector] public bool isBreaking;

        // Settings
        [SerializeField] private float motorForce, breakForce, maxSteerAngle;

        // Wheel Colliders
        [SerializeField] private List<WheelCollider> frontWheels;
        [SerializeField] private List<WheelCollider> rearWheels;

        // Wheels
        [SerializeField] private List<Transform> frontWheelTransforms;
        [SerializeField] private List<Transform> rearWheelTransforms;

        private void FixedUpdate() {
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }

        private void HandleMotor() {
            foreach (var wheelCollider in frontWheels)
            {
                wheelCollider.motorTorque = verticalInput * motorForce;    
            }
            currentbreakForce = isBreaking ? breakForce : (verticalInput != 0 ? 0.0f : 0.01f);
            ApplyBreaking();
        }

        private void ApplyBreaking() {
            foreach (var wheelCollider in frontWheels)
            {
                wheelCollider.brakeTorque = currentbreakForce;    
            }
            foreach (var wheelCollider in rearWheels)
            {
                wheelCollider.brakeTorque = currentbreakForce;    
            }
        }

        private void HandleSteering() {
            currentSteerAngle = maxSteerAngle * horizontalInput;
            foreach (var wheelCollider in frontWheels)
            {
                wheelCollider.steerAngle = currentSteerAngle;    
            }
        }

        private void UpdateWheels() {
            for (int i = 0; i < frontWheels.Count; i++)
            {
                UpdateSingleWheel(frontWheels[i], frontWheelTransforms[i]);
            }
            for (int i = 0; i < rearWheels.Count; i++)
            {
                UpdateSingleWheel(rearWheels[i], rearWheelTransforms[i]);
            }
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
            Vector3 pos;
            Quaternion rot; 
            wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }
    }
}