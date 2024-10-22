using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.locations.b_point
{
    public class PointBLocation : Location
    {
        [SerializeField] private Transform exit1;
        [SerializeField] private GameObject nextLocationPrefab;
        
        private Transform generatedRoadExit;

        [Button]
        public override void Init()
        {
            GenerateRoad();
        }

        private void GenerateRoad()
        {
            roadGenerator.Generate(10, exit1);
            generatedRoadExit = roadGenerator.lastRoadTileEnd;
        }

        public override void OnExit(string exitName)
        {
            LocationManager.Instance.LoadLocation(nextLocationPrefab, generatedRoadExit);
        }
    }
}