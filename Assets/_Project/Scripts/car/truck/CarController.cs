using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.locations;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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


        [SerializeField] private Transform trailerConnector;
        [SerializeField] private float trailerDetectorRadius = 0.7f;
        [SerializeField] public TrailerScript trailer;
        private Rigidbody rigidBody;
        private Vector3 centerOfMassDefault = new Vector3(0, 0.59f, -0.59f);
        private Vector3 centerOfMassWithTrailer = new Vector3(0, 0.59f, 1.47f);

        [SerializeField] private Transform parentForTrailers;

        [SerializeField] private GameObject[] backLights;
        
        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rigidBody.centerOfMass = centerOfMassDefault;
            
            if (trailer && !trailer.IsAttached())
                InitStartingTrailer();
        }

        private void FixedUpdate()
        {
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

        private void InitStartingTrailer() 
        {
            trailer.ActivateHinge(trailerConnector, rigidBody);
            rigidBody.centerOfMass = centerOfMassWithTrailer;
            trailer.transform.SetParent(parentForTrailers);
            
            foreach (var backLight in backLights)
            {
                backLight.SetActive(false);
            }
            
            EventBus<MissionUpdate>.Pub(new MissionUpdate(true));
        }

        public void AttachDetachTrailer()
        {
            if (this.trailer == null)
            {
                var hits = Physics.SphereCastAll(trailerConnector.position, trailerDetectorRadius, -transform.up, 0.1f); 
                if (hits.Length > 0)
                {
                    hits.FirstOrDefault(raycastHit =>
                    {
                        if (raycastHit.transform.gameObject.TryGetComponent(out TrailerScript trailer))
                        {
                            if (trailer.enabled)
                            {
                                trailer.ActivateHinge(trailerConnector, rigidBody);
                                this.trailer = trailer;
                                rigidBody.centerOfMass = centerOfMassWithTrailer;
                                trailer.transform.SetParent(parentForTrailers);
                                
                                foreach (var backLight in backLights)
                                {
                                    backLight.SetActive(false);
                                }
                                
                                EventBus<MissionUpdate>.Pub(new MissionUpdate(true));
                                return true;    
                            }
                        }
                        return false;
                    });
                }    
            }
            else
            {
                trailer.DeactivateHinge();
                
                var hits = Physics.SphereCastAll(trailerConnector.position, 5.0f, -transform.up, 10.0f); 
                if (hits.Length > 0)
                {
                    hits.FirstOrDefault(raycastHit =>
                    {
                        var loca = raycastHit.transform.gameObject.GetComponentInParent<Location>();
                        if (loca)
                        {
                            trailer.transform.SetParent(loca.transform);
                            return true; 
                        }
                        return false;
                    });
                }
                
                trailer = null;
                rigidBody.centerOfMass = centerOfMassDefault;
                
                foreach (var backLight in backLights)
                {
                    backLight.SetActive(true);
                }
                
                EventBus<MissionUpdate>.Pub(new MissionUpdate(false));
            }
        }
    }
}