using UnityEngine;

namespace _Project.Scripts
{
    public class ControlWheel : MonoBehaviour
    {
        [SerializeField] private CarController carController;
        private Quaternion initialRotation;
    
        // Start is called before the first frame update
        void Start()
        {
            initialRotation = transform.localRotation;
            UpdateSteeringAngle();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateSteeringAngle();
        }

        private void UpdateSteeringAngle()
        {
            var euler = initialRotation.eulerAngles;
            transform.localEulerAngles = transform.up * (carController.currentSteerAngle);
        }
    }
}
