using System;
using _Project.Scripts.locations.road;
using _Project.Scripts.scriptables;
using UnityEngine;

namespace _Project.Scripts.locations
{
    public abstract class Location: MonoBehaviour
    {
        public Transform start;
        public RoadGenerator roadGenerator;
        public virtual void Init()
        {
            
        }
        
        public virtual void DeInit()
        {
            
        }

        public virtual void OnExit(String exitName)
        {
            
        }
        
    }
}