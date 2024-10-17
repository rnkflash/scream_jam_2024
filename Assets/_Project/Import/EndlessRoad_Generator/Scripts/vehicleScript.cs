//heavy modified by Boris Chuprin smokerr@mail.ru
//this script is for traffic vehicles driven by AI
//it's based on classic Unity's WheelColliders script
//added AI part and dust paricles generator

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor;
	public bool steering;
}

public class vehicleScript : MonoBehaviour {
	
	
	//creating arrya of waypoints
	private List<GameObject> targetsWPList = new List<GameObject>();
	private GameObject mostClosestGO;
	private float mostClosestDist;
	public string nameWP_GO;//this variable will be assigned in real-time, just after decision left or righ lane this car takes 
	
	public int motorPower;
	private int standardMotorPower = 500;
	public int brakePower;//don't really need it. AI is silly one - don't use brakes.
	private int standardBrakePower = 1000;//don't really need it. AI is silly one - don't use brakes.
	
	private GameObject playerDriver;//this variable is player's vehicle. If distance is too far we'll destroy THIS vehicle because if player can't
	//see it, he didn't need it
	
	public List<AxleInfo> axleInfos; 
	public float maxSteeringAngle;
	
	private Transform targetWP;
	
	private float steering = 0f;//tmp steering angle when born
	
	private Vector3 dir;
	private float angle;

	public ParticleSystem dustGenerator;//for dust particles generator
	

	public void ApplyLocalPositionToVisuals(WheelCollider collider)
	{
		if (collider.transform.childCount == 0) {
			return;
		}
		
		Transform visualWheel = collider.transform.GetChild(0);
		
		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);
		
		visualWheel.transform.position = position;
		visualWheel.transform.rotation = rotation;

	}
	
	public void Start(){

		//if somehow(!) AI 'don't know' his lane, he is on right lane
		if (nameWP_GO != "laneR1" && nameWP_GO != "laneL1") {
			nameWP_GO = "laneR1";
		}

		motorPower = standardMotorPower;

		mostClosestDist = 150f;//waypoint search range is 150 meters. Why not ?

		//looking for most closest waypont of vehicle lane(left or right)
		foreach(GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()){
			if(nameWP_GO == "laneR1"){
				if(gameObj.name == "laneR1"){
					targetsWPList.Add (gameObj);
				}
			}
			if(nameWP_GO == "laneL1"){
				if(gameObj.name == "laneL1"){
					targetsWPList.Add (gameObj);
				}
			}
		}

		//seek through array of waypoints
		for (int i = 0; i < targetsWPList.Count; i++) {
			float dist = Vector3.Distance(targetsWPList[i].transform.position, transform.position);
			if (dist < mostClosestDist){
				mostClosestGO = targetsWPList[i];
				mostClosestDist = dist;
			}
		}
		targetWP = mostClosestGO.transform;

		//DEBUG - recolor target waypoint in red color(works when waypoints is a meshes)
		//targetWP.GetComponent<Renderer> ().materials [0].color = new Color (1f, 0.2f, 0.2f, 1f);//debug red color of target
		
		foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			if (gameObj.name == "player_vehicle") {
				playerDriver = gameObj;
			}
		}
	}
	
	
	public void FixedUpdate()
	{
		
		foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motorPower;
				axleInfo.rightWheel.motorTorque = motorPower;
				axleInfo.leftWheel.brakeTorque = brakePower;
				axleInfo.rightWheel.brakeTorque = brakePower;
			}
			ApplyLocalPositionToVisuals (axleInfo.leftWheel);
			ApplyLocalPositionToVisuals (axleInfo.rightWheel);
		}
		

		//AI section///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////
		//sometimes, somehow AI may lost next waypoint - far away from player road segments with WP destorying
		//in that situation, when next waypoint is lost we should destroy this vehicle to save memory
		if (targetWP) {//if not - 'else Destroy (gameObject);'
			//steering to existing waypoint
			dir = targetWP.transform.position - transform.position;
			dir = transform.InverseTransformDirection (dir);
			angle = Mathf.Atan2 (dir.x, dir.z) * Mathf.Rad2Deg;
			steering = Mathf.Clamp (angle, -maxSteeringAngle, maxSteeringAngle);
			
			motorPower = standardMotorPower - Mathf.Abs (Mathf.FloorToInt (steering) * 5);
			if (motorPower < 0)
				motorPower = 0;
			
			//scan distance to waypoint. if less than 5 meters, then looking for new one waypoint.
			if (Vector3.Distance (targetWP.transform.position, transform.position) < 5) {

				//DEBUG - recolor target waypoint in red color(works when waypoints is a meshes)
				//targetWP.GetComponent<Renderer> ().materials [0].color = new Color (1f, 1f, 1f, 1f);//debug back to white color of target
				ScanForNextWP ();
			}
			
			//destroy vehicle if it's far than 200 meters to save memory
			if (Vector3.Distance (transform.position, playerDriver.transform.position) > 200) {
				Destroy (gameObject);
			}
		} else {
			//DEBUG
			//print ("I'm lost, so bye");
			Destroy (gameObject);//situation when vehicle lost
		}

		//generates particles when offroad
		if (dustGenerator) {
			RaycastHit hit;
			if (Physics.Raycast (dustGenerator.transform.position, -Vector3.up, out hit)) {
				if (hit.transform.GetComponent<MeshRenderer> ()) {//test for meshrender
					if (hit.transform.GetComponent<MeshRenderer> ().material.name != "road_asphalt_mat (Instance)") {//take attention to material name:
						//road_asphalt_mat (Instance) - it's name of meterial of road. If vehicle 'feels' this material under wheels then dust wont
						//be created. If there is no 'road_asphalt_mat (Instance)' the vehicle will produce dust particles because car is off-road.
						dustGenerator.enableEmission = true;
						dustGenerator.emissionRate = Mathf.Round ((GetComponent<Rigidbody> ().velocity.magnitude * 3.6f) * 10) * 0.1f / 2;//from m/s to km/hh and
						//devide by 2 is for less particles at high speed. We don't really need 100 particles. it's too much.
					} else
						dustGenerator.enableEmission = false;
				}
			}
		}

	}
	
	void ScanForNextWP(){
		targetsWPList.Clear();
		mostClosestDist = 150;

		foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			if(nameWP_GO == "laneR1"){
				if(gameObj.name == "laneR1"){
					targetsWPList.Add (gameObj);
				}
			}
			if(nameWP_GO == "laneL1"){
				if(gameObj.name == "laneL1"){
					targetsWPList.Add (gameObj);
				}
			}
		}
		mostClosestGO = null;//need to reset current waypoint
		for (int i = 0; i < targetsWPList.Count; i++) {
			float dist = Vector3.Distance (targetsWPList [i].transform.position, transform.position);
			if (dist > 6 && dist < mostClosestDist) {
				dir = targetsWPList [i].transform.position - transform.position;
				dir = transform.InverseTransformDirection(dir);
				angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
				if (angle > -90 && angle <90){//looking for new waypoint in 180 degrees(we don't want to find waypoint at 6)
					mostClosestGO = targetsWPList [i];
					mostClosestDist = dist;
				}
			}
		}
		if (mostClosestGO != null) {
			targetWP = mostClosestGO.transform;
			//DEBUG changing color of waypoint if it's mesh(for debug)
			//targetWP.GetComponent<Renderer> ().materials [0].color = new Color (1f, 0.2f, 0.2f, 1f);//debug red color of target;
		} else {
			standardMotorPower = 0;//turn off acceleration because there is no waypoints ahead
			brakePower = standardBrakePower;
		}
	}
}