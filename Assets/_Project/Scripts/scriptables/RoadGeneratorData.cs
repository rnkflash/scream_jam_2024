using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.scriptables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/RoadGeneratorData")]
    public class RoadGeneratorData : SerializedScriptableObject
    {
        //straight road segment
        public GameObject roadPref_Straight;

        //light turns
        public GameObject roadPrefR_Light;
        public GameObject roadPrefL_Light;

        //easy turns
        public GameObject roadPrefR_Easy;
        public GameObject roadPrefL_Easy;

        //medium turns
        public GameObject roadPrefR_Medium;
        public GameObject roadPrefL_Medium;

        //hard turns
        public GameObject roadPrefR_Hard;
        public GameObject roadPrefL_Hard;

        //extreme turns
        public GameObject roadPrefR_Extreme;
        public GameObject roadPrefL_Extreme;

        //hills
        public GameObject roadPref_Up;
        public GameObject roadPref_Down;

        //lean fix(described in documentation) - special segments to fix horizontal lean of road
        public GameObject roadPref_LeanR;
        public GameObject roadPref_LeanL;


        //array container for hills and mounts aside of road
        public List<GameObject> hillsCount;


        //array container for obstacles
        public List<GameObject> obstaclesCount;


        //array container for props aside of road
        public List<GameObject> propsCount;


        //array container for traffic vehicle prefab
        public List<GameObject> trafficCount;


        //this is scrollers in Inspector to make initial road setup or edit it in runtime.
        [Range(0, 100)]
        public int roadHardess; //road curvature scroller in Inspector. 0 - straight road, 100 - only extreme turns.

        [Range(0, 100)] public int hillsPercentage; //frequency of hills. 100 means every road segment is hill.

        [Range(0, 100)]
        public int
            traffic; //percentage of possible traffic(per new generated road segment). 0 - no traffic at all, 100 - one random car on every new road segment.

        [Range(0, 100)]
        public int
            spoilers; //percentage of possible road obstacles. 0 - no obstacles, 100 - random obstacle on every new road segment.

        [Range(0, 100)]
        public int
            sideProps; //percentage of possible pros aside of road. 0 - no props, 100 - random props on every new road segment.

        public int maxRoadHeight; //possible height of road. It means how high or how low your hills or moutains can be.
    }
}