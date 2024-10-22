using System;
using UnityEngine;

namespace _Project.Scripts.car.trailer
{
    public class ParkingLeg : MonoBehaviour
    {
        private TrailerScript trailer;
        private FixedJoint fixedJoint;
        private Collider collider;

        private void Awake()
        {
            trailer = GetComponentInParent<TrailerScript>();
            collider = GetComponent<Collider>();
        }

        private void Start()
        {
            SetColliderActive(true);
        }

        public void SetColliderActive(bool huh)
        {
            collider.enabled = huh;
            //ActivateFixedJoint(huh);
        }
        
        public void ActivateFixedJoint(bool activate)
        {
            if (activate)
            {
                if (!fixedJoint)
                {
                    fixedJoint = gameObject.AddComponent<FixedJoint>();
                    fixedJoint.connectedBody = trailer.GetComponent<Rigidbody>();    
                }
            }
            else
            {
                if (fixedJoint)
                {
                    Destroy(fixedJoint);
                    fixedJoint = null;    
                }
            }
        } 
    }
}
