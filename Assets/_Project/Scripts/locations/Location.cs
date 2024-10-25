using System;
using System.Collections.Generic;
using _Project.Scripts.player;
using _Project.Scripts.road;
using _Project.Scripts.scriptables;
using UnityEngine;

namespace _Project.Scripts.locations
{
    public abstract class Location: MonoBehaviour
    {
        public Transform start;
        public RoadGenerator roadGenerator;
        [SerializeField] private List<GameObject> disableTheseObjects = new List<GameObject>();
        private bool objectsDisabled;
        public virtual void Init()
        {
            
        }
        
        public virtual void DeInit()
        {
            
        }

        public virtual void OnExit(String exitName)
        {
            
        }

        private void FixedUpdate()
        {
            var player = PlayerGlobal.Instance.currentPlayerPosition;
            var threshold = PlayerGlobal.Instance.disableObjectsOnDistance;
            var dist = Vector3.Distance(this.transform.position, player.position);
            var playerIsTooFar = dist > threshold;
            if (objectsDisabled != playerIsTooFar)
            {
                objectsDisabled = playerIsTooFar;
                foreach (var obj in disableTheseObjects)
                {
                    obj.SetActive(!playerIsTooFar);
                }
            }
        }
    }
}