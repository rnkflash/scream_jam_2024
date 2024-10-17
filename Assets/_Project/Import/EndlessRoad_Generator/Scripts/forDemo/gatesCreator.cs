//written by Boris Chuprin smokerr@mail.ru
//this script generates a finish gates after 800 meters of distance 
//written for demo

using UnityEngine;
using System.Collections;

public class gatesCreator : MonoBehaviour {

	public GameObject finishGates;//gates prefab
	public bool gatesAreEstablished = false;

	

	// Update is called once per frame
	void Update () {
	
		//will create a gates when player pass through 800 meters
		//honestly it might be any trigger or event: distance meters, collider trigger, time, anything.
		//this script was made to make any multiple events as aftermath of anything.
		//so, just universal script for any game events.
		if (GetComponent<distanceCounter> ().passedDistance > 800 && !gatesAreEstablished){
			GameObject lastRoadTile = Instantiate (finishGates, this.GetComponent<roadGenerate>().lastRoadTilePosition, Quaternion.Euler (this.GetComponent<roadGenerate>().lastRoadTileEndRotation)) as GameObject;
			gatesAreEstablished = true;
		}

	}
}
