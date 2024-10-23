using System;
using System.Collections;
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
        
        [SerializeField] private float lifeTime = 30.0f;
        private AudioSource audioSource;

        private bool isTriggered;

        private float timer;
        private State state = State.INACTIVE;

        private bool finishHunting;
        private enum State
        {
            INACTIVE, APPEARING, HUNTING
        }

        private void Start()
        {
            huntingSphere = GetComponent<SphereCollider>();
            model.SetActive(false);
            audioSource = GetComponent<AudioSource>();
            
            EventBus<AFKGhoulStopHunting>.Sub(OnMessage);
        }

        private void OnDestroy()
        {
            EventBus<AFKGhoulStopHunting>.Unsub(OnMessage);
        }

        private void OnMessage(AFKGhoulStopHunting message)
        {
            finishHunting = true;
            Disappear();
        }

        [Button]
        public void StartHunting(GameObject huntingGround, float huntingTime)
        {
            if (state != State.INACTIVE) return;
            StopAllCoroutines();
            finishHunting = false;
            
            this.huntingGround = huntingGround;

            state = State.APPEARING;
            timer = Random.Range(appearTimeMin, appearTimeMax);
            huntingSphere.radius = 1.0f;

            StartCoroutine(DisappearTimer(huntingTime));
        }
        
        private void ContinueHunting()
        {
            if (state != State.INACTIVE) return;
            state = State.APPEARING;
            timer = Random.Range(appearTimeMin, appearTimeMax);
            huntingSphere.radius = 1.0f;
        }

        private IEnumerator DisappearTimer(float huntingTime)
        {
            yield return new WaitForSeconds(huntingTime);
            finishHunting = true;
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

            if (!finishHunting) ContinueHunting();
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
