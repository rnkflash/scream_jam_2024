using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.player;
using _Project.Scripts.utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.monster
{
    public class AfkGhoul : MonoBehaviour
    {
        private GameObject huntingGround;

        [SerializeField] private float appearTimeMax = 5.0f;
        [SerializeField] private float appearTimeMin = 1.0f;
        
        [SerializeField] private float huntingTimeMax = 10.0f;
        [SerializeField] private float huntingTimeMin = 1.0f;

        private SphereCollider huntingSphere;

        [SerializeField] private GameObject model;

        [SerializeField] private AudioClip huntingRoar;
        private AudioSource audioSource;

        private bool isTriggered;

        private float timer;
        private State state = State.INACTIVE;
        private enum State
        {
            INACTIVE, APPEARING, HUNTING
        }

        private void Start()
        {
            huntingSphere = GetComponent<SphereCollider>();
            model.SetActive(false);
            audioSource = GetComponent<AudioSource>();
        }

        [Button]
        public void StartHunting(GameObject huntingGround)
        {
            if (state != State.INACTIVE) return;
            
            this.huntingGround = huntingGround;

            state = State.APPEARING;
            timer = Random.Range(appearTimeMin, appearTimeMax);
            huntingSphere.radius = 1.0f;
        }

        private void Update()
        {
            if (state == State.APPEARING)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    Appear();
                }
            }

            if (state == State.HUNTING)
            {
                timer -= Time.deltaTime;
                huntingSphere.radius += 1.0f * Time.deltaTime;

                if (timer < 0)
                {
                    Disappear();
                }
            }
        }

        private void Disappear()
        {
            state = State.INACTIVE;
            model.SetActive(false);
            huntingSphere.radius = 1.0f;

            StartHunting(huntingGround);
        }

        private void Appear()
        {
            state = State.HUNTING;
            timer = Random.Range(huntingTimeMin, huntingTimeMax);

            audioSource.PlayOneShot(huntingRoar);

            transform.position = RandomPointOnPlane.get(huntingGround);
            
            var currentCamera = Camera.main;
            var objectToLookAt = currentCamera?.transform;
            if (objectToLookAt != null)
            {
                Vector3 targetPostition = new Vector3( objectToLookAt.position.x, 
                    this.transform.position.y, 
                    objectToLookAt.position.z ) ;
                this.transform.LookAt( targetPostition ) ;
            }
            
            model.SetActive(true);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (isTriggered || state != State.HUNTING) return;
            var car = other.transform.gameObject.GetComponent<FirstPersonController>();
            if (car != null)
            {
                isTriggered = true;
                model.SetActive(false);
                EventBus<JumpScareEvent>.Pub(new JumpScareEvent());
            }
        }
    }
}
