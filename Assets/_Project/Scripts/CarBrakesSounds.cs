using UnityEngine;

namespace _Project.Scripts
{
    public class CarBrakeSounds : MonoBehaviour
    {
        public float minSpeed;
        public float maxSpeed;
        private float currentSpeed;

        [SerializeField] private Rigidbody carRb;
        [SerializeField] private CarController carController;
        
        private AudioSource audioSource;

        public float minPitch;
        public float maxPitch;
        private float pitchFromCar;

        private bool isSoundPlaying;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            isSoundPlaying = carController.isBreaking;
            
            PlaySound();
        }

        void PlaySound()
        {
            if (audioSource.isPlaying)
            {
                if (isSoundPlaying == false)
                    audioSource.Stop();
            }
            else
            {
                if (isSoundPlaying)
                    audioSource.Play();
            }
            
            if (!audioSource.isPlaying) return;
            
            var maxPitchFromCar = maxPitch - minPitch;
            
            currentSpeed = carRb.velocity.magnitude;
            pitchFromCar = maxPitchFromCar * carRb.velocity.magnitude / maxSpeed;

            if(currentSpeed < minSpeed)
            {
                audioSource.pitch = 0;
            }

            if(currentSpeed > minSpeed && currentSpeed < maxSpeed)
            {
                audioSource.pitch = minPitch + pitchFromCar;
            }

            if(currentSpeed > maxSpeed)
            {
                audioSource.pitch = maxPitch;
            }
        }
    }
}