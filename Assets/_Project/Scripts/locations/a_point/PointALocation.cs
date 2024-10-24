﻿using _Project.Scripts.player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.locations.a_point
{
    public class PointALocation : Location
    {
        [SerializeField] private Transform exit1;
        
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
            var prefab = LocationRandomizator.Instance.GetNextRandomLocationPrefab(this);
            LocationManager.Instance.LoadLocation(prefab, generatedRoadExit);
        }
    }
}