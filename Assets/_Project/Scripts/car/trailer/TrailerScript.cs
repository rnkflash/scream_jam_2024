using System.Collections.Generic;
using _Project.Scripts.car.trailer;
using _Project.Scripts.utils;
using UnityEngine;

namespace _Project.Scripts
{
    public class TrailerScript : MonoBehaviour
    {
        [SerializeField] private Transform trailerConnector;
        [SerializeField] private List<Transform> recieverPorts;
        
        [SerializeField] private Vector3 Axis;

        [SerializeField] private Vector2 Limits;

        private ParkingLeg parkingLeg;

        private HingeJoint hingeJoint;

        [SerializeField] private Light[] lights;
        
        private void CreateHingeJoint(Rigidbody connectedBody, Transform connectedAnchor)
        {
            hingeJoint = gameObject.AddComponent<HingeJoint>();
            hingeJoint.axis = Axis;
            hingeJoint.limits = new JointLimits()
            {
                min = Limits.x,
                max = Limits.y
            };
            hingeJoint.anchor = trailerConnector.localPosition;
            //hingeJoint.connectedAnchor = connectedAnchor.localPosition;
            hingeJoint.connectedBody = connectedBody;
            hingeJoint.useLimits = true;
            hingeJoint.autoConfigureConnectedAnchor = true;
        }

        private void Awake()
        {
            hingeJoint = GetComponent<HingeJoint>();
            parkingLeg = GetComponentInChildren<ParkingLeg>();
        }

        public void ActivateHinge(Transform truckConnector, Rigidbody truckRigidbody)
        {
            if (IsAttached()) return;

            AlignAtObject.AlignWithYourChildAt(transform, trailerConnector, truckConnector, Quaternion.identity);
            CreateHingeJoint(truckRigidbody, truckConnector);
            parkingLeg.gameObject.SetActive(false);
            
            foreach (var light1 in lights)
            {
                light1.renderMode = LightRenderMode.ForcePixel;
            }
        }

        public void DeactivateHinge()
        {
            if (!IsAttached()) return;
            Destroy(hingeJoint);
            hingeJoint = null;
            parkingLeg.gameObject.SetActive(true);
            
            foreach (var light1 in lights)
            {
                light1.renderMode = LightRenderMode.ForceVertex;
            }
        }

        public List<Transform> GetReceiverPorts()
        {
            return recieverPorts;
        }

        public bool IsAttached()
        {
            return hingeJoint;
        }
    }
}
