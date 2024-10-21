using System;
using System.Collections.Generic;
using _Project.Scripts.locations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.scriptables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/LocationData")]
    public class LocationData : SerializedScriptableObject
    {
        public struct ExitData
        {
            public LocationTypes type;
            public int generatedRoadLength;
        }
        
        public LocationTypes type;
        public Dictionary<String, ExitData> exits;
    }
}
