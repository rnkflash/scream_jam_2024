using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject topDownCamera;
        [SerializeField] private GameObject carInteriorCamera;
        [SerializeField] private GameObject fpsCamera;
        
        private int current;
        private Cameras currentCamera = Cameras.FPS;

        enum Cameras
        {
            FPS,
            TruckInterior,
            TruckOutside
        }
        
        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            SwitchCamera(0);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                int next = current + 1;
                if (next >= Enum.GetValues(typeof(Cameras)).Length)
                    next = 0;
                SwitchCamera(next);
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

        private void SwitchCamera(int next)
        {
            current = next;
            currentCamera = (Cameras)current;

            carInteriorCamera.SetActive(false);
            topDownCamera.SetActive(false);
            fpsCamera.SetActive(false);
            
            switch (currentCamera)
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
    }
}
