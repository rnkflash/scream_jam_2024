using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.utils
{
    public class RandomPointOnPlane
    {
        public static Vector3 get(GameObject plane)
        {
            List<Vector3> VerticeList = new List<Vector3>(plane.GetComponent<MeshFilter>().sharedMesh.vertices);
            Vector3 leftTop = plane.transform.TransformPoint(VerticeList[0]);
            Vector3 rightTop = plane.transform.TransformPoint(VerticeList[10]);
            Vector3 leftBottom = plane.transform.TransformPoint(VerticeList[110]);
            Vector3 rightBottom = plane.transform.TransformPoint(VerticeList[120]);
            Vector3 XAxis = rightTop - leftTop;
            Vector3 ZAxis = leftBottom - leftTop;
            Vector3 RndPointonPlane = leftTop + XAxis * Random.value + ZAxis * Random.value;

            return RndPointonPlane + plane.transform.up * 0.5f;
        }
    }
}