using System;
using _Project.Scripts.enums;
using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
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
        
        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            SwitchCamera(Cameras.FPS);
            sceneLight.SetActive(false);
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
            
            carInteriorCamera.SetActive(false);
            topDownCamera.SetActive(false);
            fpsCamera.SetActive(false);
            
            switch (camera)
            {
                case Cameras.FPS:
                    fpsCamera.SetActive(true);
                    break;
                case Cameras.TruckInterior:
                    carInteriorCamera.SetActive(true);
                    break;
                case Cameras.TruckOutside:
                    topDownCamera.SetActive(true);
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
