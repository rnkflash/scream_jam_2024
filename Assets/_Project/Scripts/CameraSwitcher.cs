using System;
using _Project.Scripts.enums;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.player;
using _Project.Scripts.utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class CameraSwitcher : Singleton<CameraSwitcher>
    {
        [SerializeField] private GameObject topDownCamera;
        [SerializeField] private GameObject carInteriorCamera;
        [SerializeField] private GameObject fpsCamera;
        [SerializeField] private GameObject sceneLight;
        [SerializeField] private bool lockCursor = true;
        
        void Start()
        {
            Cursor.visible = false;
            LockCursor();
            SwitchCamera(Cameras.FPS);
            sceneLight.SetActive(false);
            
            EventBus<AFKGhoulStopHunting>.Pub(new AFKGhoulStopHunting());
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            LockCursor();
        }

        private void LockCursor()
        {
            if (lockCursor)
                Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                sceneLight.SetActive(!sceneLight.activeSelf);
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Screen.fullScreenMode == FullScreenMode.Windowed)
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                else 
                    Screen.fullScreenMode = FullScreenMode.Windowed;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void SwitchCamera(Cameras camera)
        {
            EventBus<NoInteractableOnTarget>.Pub(new NoInteractableOnTarget());
            
            switch (camera)
            {
                case Cameras.FPS:
                    fpsCamera.SetActive(true);
                    carInteriorCamera.SetActive(false);
                    topDownCamera.SetActive(false);
                    PlayerGlobal.Instance.currentPlayerPosition = fpsCamera.transform;
                    break;
                case Cameras.TruckInterior:
                    carInteriorCamera.SetActive(true);
                    fpsCamera.SetActive(false);
                    topDownCamera.SetActive(false);

                    PlayerGlobal.Instance.currentPlayerPosition = carInteriorCamera.transform;
                    break;
                case Cameras.TruckOutside:
                    topDownCamera.SetActive(true);
                    carInteriorCamera.SetActive(false);
                    fpsCamera.SetActive(false);
                    
                    PlayerGlobal.Instance.currentPlayerPosition = topDownCamera.GetComponent<TopDownCamera>().getCar().transform;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public GameObject GetFPSController()
        {
            return fpsCamera;
        }
    }
}
