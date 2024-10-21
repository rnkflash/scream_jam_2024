using _Project.Scripts.enums;
using UnityEngine;

namespace _Project.Scripts
{
    public class TruckInteriorCamera : MonoBehaviour
    {
        private float freeLookSensitivity = 1.0f;
        [SerializeField] private Transform interiorCameraFreeLookTransform;
        [SerializeField] private CarController car;

        void Start()
        {
        
        }

        void Update()
        {
            UpdateCarInput();
            UpdateMouseLooking();
        }

        private void UpdateMouseLooking()
        {
            float newRotationX = interiorCameraFreeLookTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
            float newRotationY = interiorCameraFreeLookTransform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
            interiorCameraFreeLookTransform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }
        
        private void UpdateCarInput() {
            // Steering Input
            car.horizontalInput = Input.GetAxis("Horizontal");

            // Acceleration Input
            car.verticalInput = Input.GetAxis("Vertical");

            // Breaking Input
            car.isBreaking = Input.GetKey(KeyCode.Space);

            if (Input.GetKeyDown(KeyCode.C))
            {
                CameraSwitcher.Instance.SwitchCamera(Cameras.TruckOutside);
            }
        }
    }
}
