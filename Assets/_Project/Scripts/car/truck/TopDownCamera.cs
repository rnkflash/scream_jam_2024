using _Project.Scripts;
using _Project.Scripts.enums;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] private CarController car;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateCarInput();
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
            CameraSwitcher.Instance.SwitchCamera(Cameras.TruckInterior);
        }
    }
}
