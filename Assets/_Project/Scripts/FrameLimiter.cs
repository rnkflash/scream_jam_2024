using UnityEngine;

namespace _Project.Scripts
{
    public class FrameLimiter : MonoBehaviour
    {
        public int target = 60;
        public int vSyncCount = 0;

        void Awake()
        {
            QualitySettings.vSyncCount = vSyncCount;
            Application.targetFrameRate = target;
        }

        void Update()
        {
            Application.targetFrameRate = target;
        }
    }
}
