using UnityEngine;

namespace _Project.Scripts
{
    public class CarSounds : MonoBehaviour
    {
        public float minSpeed;
        public float maxSpeed;
        private float currentSpeed;

        [SerializeField] private Rigidbody carRb;
        private AudioSource carAudio;

        public float minPitch;
        public float maxPitch;
        private float pitchFromCar;

        void Start()
        {
            carAudio = GetComponent<AudioSource>();
        }

        void Update()
        {
            EngineSound();
        }

        void EngineSound()
        {
            var maxPitchFromCar = maxPitch - minPitch;
            
            currentSpeed = carRb.velocity.magnitude;
            pitchFromCar = maxPitchFromCar * carRb.velocity.magnitude / maxSpeed;

            if(currentSpeed < minSpeed)
            {
                carAudio.pitch = minPitch;
            }

            if(currentSpeed > minSpeed && currentSpeed < maxSpeed)
            {
                carAudio.pitch = minPitch + pitchFromCar;
            }

            if(currentSpeed > maxSpeed)
            {
                carAudio.pitch = maxPitch;
            }
        }
    }
}