using _Project.Scripts;
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
    }
}
