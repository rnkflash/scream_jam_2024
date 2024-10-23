using UnityEngine;

namespace _Project.Scripts.monster
{
    public class AfkGhoul : MonoBehaviour
    {
        [SerializeField] private Plane huntingGround;

        private float moveTimeMax = 5.0f;
        private float moveTimeMin = 1.0f;

    }
}
