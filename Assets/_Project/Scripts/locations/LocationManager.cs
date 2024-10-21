using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.locations
{
    public class LocationManager : Singleton<LocationManager>
    {
        [SerializeField] private int locationsLimit = 2;
        private List<Location> loadedLocations = new ();

        private void Start()
        {
            foreach(Transform child in transform)
            {
                var location = child.gameObject.GetComponent<Location>(); 
                if (location != null)
                {
                    loadedLocations.Add(location);
                }
            }
            
            loadedLocations.ForEach(location => location.Init());
        }

        [Button]
        public void LoadLocation(GameObject locationPrefab, Transform previousLocationExit)
        {
            var locationObject = Instantiate(locationPrefab, transform);
            Location nextLocation = locationObject.GetComponent<Location>();
            AlignAtObject.AlignWithYourChildAt(nextLocation.transform, nextLocation.start.transform,
                previousLocationExit.transform, Quaternion.identity);
            nextLocation.Init();
            
            loadedLocations.Add(nextLocation);
            
            if (loadedLocations.Count > locationsLimit)
            {
                var toRemove = loadedLocations.First();
                loadedLocations.Remove(toRemove);
                toRemove.DeInit();
                Destroy(toRemove.gameObject);
            }
        }
    }
}
