//heavy modified by Boris Chuprin smokerr@mail.ru
//this script is for two cameras swaping(chase and orbit)
//it's based on 'classic' Unity's Camera chase script

using UnityEngine;
using System.Collections;

public class camSwitcher : MonoBehaviour
{
	public Camera backCamera;
	public Camera aroundCamera;
	public Transform cameraTarget;
	private Camera currentCamera;
	//////////////////// for chase Camera 
	public float dist = 8.0f;
	public float height = 2.0f;
	//////////////////// for orbit Camera
	private float distance = 3.0f;
	private float xSpeed = 10.0f;
	private float ySpeed = 10.0f;
	
	private float yMinLimit = -90;
	private float yMaxLimit = 90;
	
	private float distanceMin = 2;
	private float distanceMax = 10;
	
	private float x = 0.0f;
	private float y = 0.0f;
	
	private float smoothTime = 0.2f;
	
	private float xSmooth = 0.0f;
	private float ySmooth = 0.0f; 
	private float xVelocity = 0.0f;
	private float yVelocity = 0.0f;



	//////////////////////////////////////////////////////////////////////////////
	Vector3 offset;
	
	// Use this for initialization
	void Start ()
	{
		backCamera.enabled = true;
		aroundCamera.enabled = false;
		currentCamera = backCamera;
		
		if (GetComponent<Rigidbody> ())
			GetComponent<Rigidbody> ().freezeRotation = true;


		//////////////////////////////////////////////////////////////////////////////
		offset = cameraTarget.transform.position - transform.position;
	
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		//switch to orbit camera
		if (Input.GetMouseButton (1)) {
			backCamera.enabled = false;
			aroundCamera.enabled = true;
			backCamera.gameObject.SetActive (false);
			aroundCamera.gameObject.SetActive (true);
			currentCamera = aroundCamera;

			x += Input.GetAxis ("Mouse X") * xSpeed;
			y -= Input.GetAxis ("Mouse Y") * ySpeed;
			
			y = Mathf.Clamp (y, yMinLimit, yMaxLimit);

			xSmooth = Mathf.SmoothDamp (xSmooth, x, ref xVelocity, smoothTime);
			ySmooth = Mathf.SmoothDamp (ySmooth, y, ref yVelocity, smoothTime);
						
			distance = Mathf.Clamp (distance + Input.GetAxis ("Mouse ScrollWheel") * distance, distanceMin, distanceMax);
						
			currentCamera.transform.localRotation = Quaternion.Euler (ySmooth, xSmooth, 0);
			currentCamera.transform.position = currentCamera.transform.rotation * new Vector3 (0.0f, 0.0f, -distance) + cameraTarget.position;
			
		} else {//switch to chase camera
			backCamera.enabled = true;
			aroundCamera.enabled = false;
			backCamera.gameObject.SetActive (true);
			aroundCamera.gameObject.SetActive (false);
			currentCamera = backCamera;

			backCamera.fieldOfView = backCamera.fieldOfView + Input.GetAxis ("Vertical") / 5;//changing FoV based on acceleration(kinda fake)
			if (backCamera.fieldOfView > 85) {
				backCamera.fieldOfView = 85;
			}
			if (backCamera.fieldOfView < 50) {
				backCamera.fieldOfView = 50;
			}
			if (backCamera.fieldOfView < 60) {
				backCamera.fieldOfView = backCamera.fieldOfView += 0.1f;
			}
			if (backCamera.fieldOfView > 60) {
				backCamera.fieldOfView = backCamera.fieldOfView -= 0.1f;
			}


			/*
			Vector3 wantedPosition;
			wantedPosition = cameraTarget.TransformPoint(0, height, -distance);
			
			transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * 5);

			Quaternion wantedRotation = Quaternion.LookRotation(cameraTarget.position - transform.position, cameraTarget.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * 10);
			*/



			float heightCorrenction = 0;
			//camera height compensation when road going too low(hard downhill) 
			if (cameraTarget.transform.localEulerAngles.x > 10 && cameraTarget.transform.localEulerAngles.x <= 180){
				heightCorrenction = (cameraTarget.transform.localEulerAngles.x - 10)/6;//slightly add camera height when vehicle riding downhill
			} else if (cameraTarget.transform.localEulerAngles.x > 180 && cameraTarget.transform.localEulerAngles.x < 350){
				heightCorrenction = (cameraTarget.transform.localEulerAngles.x - 350)/6;//slightly subtract camera height when vehicle climbing up
			}


			float wantedRotationAngle = cameraTarget.eulerAngles.y;
			float currentRotationAngle = currentCamera.transform.eulerAngles.y;;
			float currentHeight = cameraTarget.position.y + height + heightCorrenction;//for climbs compensation

			
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, 3 * Time.deltaTime);
			
			Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
			currentCamera.transform.position = cameraTarget.position;
			currentCamera.transform.position -= currentRotation * Vector3.forward * dist;
			currentCamera.transform.position = new Vector3 (currentCamera.transform.position.x, currentHeight, currentCamera.transform.position.z);
			currentCamera.transform.LookAt (cameraTarget);

		}
	}
}