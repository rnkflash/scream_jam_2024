using System.Collections.Generic;
using _Project.Scripts.locations;
using _Project.Scripts.utils;
using UnityEngine;

namespace _Project.Scripts.player
{
    public class LocationRandomizator: Singleton<LocationRandomizator>
    {
        [SerializeField] private List<GameObject> locationPrefabs;
        [SerializeField] private GameObject PointAPrefab;
        [SerializeField] private GameObject PointBPrefab;
        private List<GameObject> lastRandomLocations = new List<GameObject>();

        public GameObject GetNextRandomLocationPrefab(Location from)
        {
            var newList = new List<GameObject>();
            newList.AddRange(locationPrefabs.FindAll(o => !lastRandomLocations.Contains(o)));
            
            if (PlayerGlobal.Instance.isOnMission)
            {
                newList.Add(PointAPrefab);
            }
            else
            {
                newList.Add(PointBPrefab);
            }

            var random = Random.Range(0, newList.Count);
            var location = newList[random];

            if (location == PointAPrefab || location == PointBPrefab)
                lastRandomLocations.Clear();
            else
                lastRandomLocations.Add(location);
            
            return location;
        }
    }
}