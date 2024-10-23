using System;
using System.Linq;
using _Project.Scripts.road;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project.Scripts.car.truck
{
    public class WheelController: MonoBehaviour
    {
        private WheelCollider wheelCollider;

        private void Awake()
        {
            wheelCollider = GetComponent<WheelCollider>();
        }

        private void FixedUpdate()
        {
            Debug.DrawLine(transform.position, transform.position -transform.up * 5.0f, Color.green );
            var hits = Physics.RaycastAll(transform.position, -transform.up, 5.0f);
            hits.Sort((hit, raycastHit) =>
            {
                if (hit.distance > raycastHit.distance)
                    return 1;
                if (hit.distance < raycastHit.distance)
                    return -1;
                return 0;

            });
            if (hits.Length > 0)
            {
                var stiffnessAdjusted = false;
                hits.FirstOrDefault(raycastHit =>
                    {
                        var roadTerrain = raycastHit.transform.gameObject.GetComponent<RoadTerrain>();
                        if (roadTerrain)
                        {
                            if (!stiffnessAdjusted)
                            {
                                var forwardFriction = wheelCollider.forwardFriction;
                                forwardFriction.stiffness = roadTerrain.stiffness;
                                wheelCollider.forwardFriction = forwardFriction;

                                var sidewaysFriction = wheelCollider.sidewaysFriction;
                                sidewaysFriction.stiffness = roadTerrain.stiffness;
                                wheelCollider.sidewaysFriction = sidewaysFriction;
                                
                                stiffnessAdjusted = true;    
                            }
                            return true;
                        }

                        return false;
                    }
                );
            }
        }
    }
}