//slightly modifyed by Boris Chuprin smokerr@mail.ru
//this script is for controlling a player's vehicle
//it's classic Unity's WheelColliders script with dust generator added by me


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class vehiclePlayerScript : MonoBehaviour {
	public List<AxleInfo> axleInfos; 
	public float maxMotorTorque;
	public float maxSteeringAngle;

	public ParticleSystem dustGenerator;//for dust particles


	// finds the corresponding visual wheel
	// correctly applies the transform
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
	
	public void FixedUpdate()
	{
		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		
		foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motor;
				axleInfo.rightWheel.motorTorque = motor;
			}
			ApplyLocalPositionToVisuals(axleInfo.leftWheel);
			ApplyLocalPositionToVisuals(axleInfo.rightWheel);
		}

		//generates dust particles when offroad
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
}