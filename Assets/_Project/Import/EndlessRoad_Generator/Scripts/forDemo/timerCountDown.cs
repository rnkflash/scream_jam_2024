//written by Boris Chuprin smokerr@mail.ru
//this script made for 'elimination time race' mode when player need to drive some distance before time is up
//written for demo 

using UnityEngine;
using System.Collections;

public class timerCountDown : MonoBehaviour {
	
	private float startTime;//abstract variable to get know what time is it
	private float currentTime;//abstract variable - device time
	public float nowTime;//abstract variable to count time from start of scene
	public int countDownInSeconds;//public variable to make time readuible from other scripts

	//variables to prevent race win(pass of finish gates) after time is up but screen 'fading in' 5 seconds after time is end.
	public bool endStatus = false;//public variable to make time readuible from other scripts
	public string lostStatus = null;//public variable to make time readuible from other scripts

	
	void OnGUI ()
	{
		GUIStyle middleText = new GUIStyle ();
		middleText.fontSize = 14;
		middleText.alignment = TextAnchor.MiddleCenter;
		middleText.normal.textColor = Color.black;

		var biggerMoreText = new GUIStyle ("label");
		biggerMoreText.fontSize = 100;
		biggerMoreText.alignment = TextAnchor.MiddleCenter;

		GUIStyle smallerText = new GUIStyle ();
		smallerText.fontSize = 14;
		smallerText.alignment = TextAnchor.MiddleCenter;
		smallerText.normal.textColor = Color.white;
		
		//help string
		smallerText.normal.textColor = Color.grey;
		GUI.Label (new Rect(Screen.width/2,Screen.height/1.095f,1,20),"Drive fast 1000 meters until 60 seconds !", smallerText);
		smallerText.normal.textColor = Color.white;
		GUI.Label (new Rect(Screen.width/2-1,Screen.height/1.095f-1,1,20),"Drive fast 1000 meters until 60 seconds !", smallerText);


		GUI.color = Color.black;
		GUI.Label (new Rect(Screen.width/1.1f,Screen.height/20,20,30),"Time", middleText);


		GUIStyle biggerText = new GUIStyle ();
		biggerText.fontSize = 24;
		biggerText.alignment = TextAnchor.MiddleCenter;
		biggerText.normal.textColor = Color.red;

		int minutes = Mathf.FloorToInt(nowTime / 60F);
		int seconds = Mathf.FloorToInt(nowTime - minutes * 60);
		string niceTime = string.Format("{0:00}:{1:00}", minutes, countDownInSeconds-seconds);
		if (countDownInSeconds - seconds < 11) GUI.color = Color.red;
		GUI.Label (new Rect (Screen.width / 1.1f, Screen.height / 7, 20, 30), "" + niceTime, biggerText);


		GUI.color = Color.black;
		GUI.Label (new Rect(Screen.width/3.5f,Screen.height/4,460,120),"" + lostStatus, biggerMoreText);
		GUI.color = Color.red;
		GUI.Label (new Rect(Screen.width/3.5f-1,Screen.height/4-1,460,120),"" + lostStatus, biggerMoreText);


	}

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime = Time.time;
		nowTime = (currentTime - startTime);

		//if time is up then player is lose
		if (nowTime > countDownInSeconds) {
			if (endStatus == false){
				lostStatus = "FAILED !";
				endStatus = true;
				StartCoroutine (loadScene (5));//5 seconds to calm down :) and screen fades in black
			} else {
				return;
			}
		}
	}

	private IEnumerator loadScene (float secToWait) {
		yield return new WaitForSeconds (secToWait-1);
		gameObject.AddComponent<fadeIn>();
		yield return new WaitForSeconds (1);
		Application.LoadLevel("demo_Select");//loading main menu scene again
	}

}
