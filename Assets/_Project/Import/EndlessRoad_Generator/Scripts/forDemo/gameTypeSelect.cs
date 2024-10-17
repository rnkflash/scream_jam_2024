//written by Boris Chuprin smokerr@mail.ru
//this script of main menu of demo when player choosing type of demo he want to watch
//written for demo

using UnityEngine;
using System.Collections;

public class gameTypeSelect : MonoBehaviour {

	void OnGUI ()
	{
		GUIStyle biggerText = new GUIStyle ();
		biggerText.fontSize = 40;
		biggerText.alignment = TextAnchor.MiddleCenter;

		GUIStyle mediumText = new GUIStyle ();
		mediumText.fontSize = 30;
		mediumText.alignment = TextAnchor.MiddleCenter;

		GUIStyle smallerText = new GUIStyle ();
		smallerText.fontSize = 16;
		smallerText.alignment = TextAnchor.MiddleCenter;

		mediumText.normal.textColor = Color.black;
		GUI.Label (new Rect (Screen.width / 2.2f, Screen.height / 20, 80, 20), "Welcome to", mediumText);
		GUI.Label (new Rect (Screen.width / 2.2f, Screen.height / 7, 80, 20), "ENDLESS ROAD", mediumText);
		GUI.Label (new Rect (Screen.width / 2.2f, Screen.height / 4.5f, 80, 20), "generator", mediumText);
		mediumText.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width / 2.2f-2, Screen.height / 20-2, 80, 20), "Welcome to", mediumText);
		GUI.Label (new Rect (Screen.width / 2.2f-2, Screen.height / 7-2, 80, 20), "ENDLESS ROAD", mediumText);
		GUI.Label (new Rect (Screen.width / 2.2f-2, Screen.height / 4.5f-2, 80, 20), "generator", mediumText);

		biggerText.normal.textColor = Color.grey;
		GUI.Label (new Rect (Screen.width / 6, Screen.height / 3, 100, 120), "Endless", biggerText);
		GUI.Label (new Rect (Screen.width / 6, Screen.height / 2.2f, 100, 120), "RUN", biggerText);
		smallerText.normal.textColor = Color.grey;
		GUI.Label (new Rect (Screen.width / 6, Screen.height / 1.9f, 100, 120), "(click here)", smallerText);
		biggerText.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width / 6-1, Screen.height / 3-1, 100, 120), "Endless", biggerText);
		GUI.Label (new Rect (Screen.width / 6-1, Screen.height / 2.2f-1, 100, 120), "RUN", biggerText);
		smallerText.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width / 6-1, Screen.height / 1.9f-1, 100, 120), "(click here)", smallerText);

		biggerText.normal.textColor = Color.grey;
		GUI.Label (new Rect (Screen.width / 1.5f, Screen.height / 3, 100, 120), "Eliminate", biggerText);
		GUI.Label (new Rect (Screen.width / 1.5f, Screen.height / 2.2f, 100, 120), "RACE", biggerText);
		smallerText.normal.textColor = Color.grey;
		GUI.Label (new Rect (Screen.width / 1.5f, Screen.height / 1.9f, 100, 120), "(click here)", smallerText);
		biggerText.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width / 1.5f-1, Screen.height / 3-1, 100, 120), "Eliminate", biggerText);
		GUI.Label (new Rect (Screen.width / 1.5f-1, Screen.height / 2.2f-1, 100, 120), "RACE", biggerText);
		smallerText.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width / 1.5f-1, Screen.height / 1.9f-1, 100, 120), "(click here)", smallerText);
		
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (ray, out hit);
			if (hit.transform.gameObject.name == "AIrunning_Selector") {
				gameObject.AddComponent<fadeIn>();
				StartCoroutine(sceneLoader("demo_curvatureControls"));
			}
			if (hit.transform.gameObject.name == "countDown_Selector") {
				gameObject.AddComponent<fadeIn>();
				StartCoroutine(sceneLoader("demo_countDown"));
			}
		}
	
	}
	private IEnumerator sceneLoader(string sceneName) {
		yield return new WaitForSeconds(1);
		Application.LoadLevel(sceneName);
	}
}
