//written by Boris Chuprin smokerr@mail.ru
//this script traveled distance in meters
//written for demo
//also, this is race interface and dashboard script

using UnityEngine;
using System.Collections;

public class distanceCounter : MonoBehaviour {

	private GameObject rider;//player gameObject
	private int riderSpeed;//speed of player
	public int passedDistance;//traveled distance - public to make it readible from other scripts
	
	private Vector3 previousPosition;//variable to keep the previous position
	private float cumulatedDistance;// traveled distance


	void OnGUI ()
	{
		GUIStyle middleText = new GUIStyle ();
		middleText.fontSize = 14;
		middleText.alignment = TextAnchor.MiddleCenter;
		middleText.normal.textColor = Color.black;

		GUIStyle biggerText = new GUIStyle ();
		biggerText.fontSize = 24;
		biggerText.alignment = TextAnchor.MiddleCenter;
		biggerText.normal.textColor = Color.black;

		GUIStyle muchbiggerText = new GUIStyle ();
		muchbiggerText.fontSize = 36;
		muchbiggerText.alignment = TextAnchor.LowerCenter;
		muchbiggerText.normal.textColor = Color.white;


		GUI.color = Color.black;
		GUI.Label (new Rect(Screen.width/20,Screen.height/20,20,30),"Distance", middleText);
		GUI.Label (new Rect(Screen.width/20,Screen.height/10,20,30),"" + passedDistance, biggerText);

		GUI.Label (new Rect(Screen.width/2,Screen.height/40,1,30),"Speed", biggerText);
		muchbiggerText.normal.textColor = Color.white;
		GUI.Label (new Rect(Screen.width/2,Screen.height/8f,1,30),"" + riderSpeed, muchbiggerText);

		//help string
		middleText.normal.textColor = Color.grey;
		GUI.Label (new Rect(Screen.width/2,Screen.height/1.048f,1,20),"Press 'Esc' for back to Main menu", middleText);

	}


	// Use this for initialization
	void Start () {
		rider = GameObject.Find ("player_vehicle");

		previousPosition = rider.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		riderSpeed = (int)(Mathf.Round((rider.GetComponent<Rigidbody>().velocity.magnitude * 3.6f)*10) * 0.1f);//from m/s to km/hh


		// add the distance traveled during the last frame
		cumulatedDistance += (rider.transform.position - previousPosition).magnitude;
		// update the 'previous' position to the current one
		previousPosition = rider.transform.position;
		passedDistance = (int)(cumulatedDistance);


	}
}
