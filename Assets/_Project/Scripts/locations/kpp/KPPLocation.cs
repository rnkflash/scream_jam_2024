using _Project.Scripts.eventbus;
using _Project.Scripts.eventbus.events;
using _Project.Scripts.player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.locations.kpp
{
    public class KPPLocation : Location
    {
        [SerializeField] private Transform exit1;
        [SerializeField] private GameObject huntingGround;
        
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
            LocationManager.Instance.LoadLocation(
                LocationRandomizator.Instance.GetNextRandomLocationPrefab(this), 
                generatedRoadExit
           );
        }

        public void ReleaseKraken()
        {
            EventBus<AFKGhoulStartHunting>.Pub(new AFKGhoulStartHunting(huntingGround));
        }
    }
}