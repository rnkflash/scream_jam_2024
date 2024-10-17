//written by Boris Chuprin smokerr@mail.ru
//this script creates onscreen visual controls(scrollers) to change road parameters in realtime
//to show how you can change everything in realtime(curvarure of road, traffic, obtacles, climbs and so on)
//written for demo 

using UnityEngine;
using System.Collections;

public class onScreenConrols : MonoBehaviour {
	
	private float curvatureSliderValue = 0.0F;
	private float hillsSliderValue = 0.0F;
	private float trafficSliderValue = 0.0F;
	private float spoilersSliderValue = 0.0F;
	private float sidepropsSliderValue = 0.0F;
	
	void OnGUI ()
	{
		GUIStyle smallerText = new GUIStyle ();
		smallerText.fontSize = 14;
		smallerText.alignment = TextAnchor.MiddleCenter;
		smallerText.normal.textColor = Color.black;

		//help string
		smallerText.normal.textColor = Color.grey;
		GUI.Label (new Rect(Screen.width/2,Screen.height/1.095f,1,20),"Use scrolls at upper right corner to change road generation settings in realtime", smallerText);
		smallerText.normal.textColor = Color.white;
		GUI.Label (new Rect(Screen.width/2-1,Screen.height/1.095f-1,1,20),"Use scrolls at upper right corner to change road generation settings in realtime", smallerText);

		smallerText.alignment = TextAnchor.MiddleCenter;
		smallerText.normal.textColor = Color.black;
		//curvature regulation
		GUI.Label (new Rect(Screen.width/1.4f,Screen.height/20,10,30),"Curvature", smallerText);
		GUI.Label (new Rect(Screen.width/1.4f,Screen.height/1.95f,10,30),"" + Mathf.Round(curvatureSliderValue), smallerText);
		curvatureSliderValue = GUI.VerticalSlider (new Rect (Screen.width / 1.4f, Screen.height / 8, 10, 150), curvatureSliderValue, 100.0F, 0.0F);

		//hills regulation
		GUI.Label (new Rect(Screen.width/1.3f,Screen.height/20,10,30),"Hills", smallerText);
		GUI.Label (new Rect(Screen.width/1.3f,Screen.height/1.95f,10,30),"" + Mathf.Round(hillsSliderValue), smallerText);
		hillsSliderValue = GUI.VerticalSlider (new Rect (Screen.width / 1.3f, Screen.height / 8, 10, 150), hillsSliderValue, 100.0F, 0.0F);

		//traffic regulation
		GUI.Label (new Rect(Screen.width/1.2f,Screen.height/20,10,30),"Traffic", smallerText);
		GUI.Label (new Rect(Screen.width/1.2f,Screen.height/1.95f,10,30),"" + Mathf.Round(trafficSliderValue), smallerText);
		trafficSliderValue = GUI.VerticalSlider (new Rect (Screen.width / 1.2f, Screen.height / 8, 10, 150), trafficSliderValue, 100.0F, 0.0F);

		//spoilers regulation
		GUI.Label (new Rect(Screen.width/1.1f,Screen.height/20,10,30),"Spoilers", smallerText);
		GUI.Label (new Rect(Screen.width/1.1f,Screen.height/3.8f,10,30),"" + Mathf.Round(spoilersSliderValue), smallerText);
		spoilersSliderValue = GUI.VerticalSlider (new Rect (Screen.width / 1.1f, Screen.height / 8, 10, 60), spoilersSliderValue, 100.0F, 0.0F);

		//sideProps regulation
		GUI.Label (new Rect(Screen.width/1.1f,Screen.height/3.2f,10,30),"SideProps", smallerText);
		GUI.Label (new Rect(Screen.width/1.1f,Screen.height/1.95f,10,30),"" + Mathf.Round(sidepropsSliderValue), smallerText);
		sidepropsSliderValue = GUI.VerticalSlider (new Rect (Screen.width / 1.1f, Screen.height / 2.7f, 10, 60), sidepropsSliderValue, 100.0F, 0.0F);


		GetComponent<roadGenerate> ().roadHardess = (int)Mathf.Round (curvatureSliderValue);
		GetComponent<roadGenerate> ().hillsPercentage = (int)Mathf.Round (hillsSliderValue);
		GetComponent<roadGenerate> ().traffic = (int)Mathf.Round (trafficSliderValue);

		GetComponent<roadGenerate> ().spoilers = (int)Mathf.Round (spoilersSliderValue);
		GetComponent<roadGenerate> ().sideProps = (int)Mathf.Round (sidepropsSliderValue);

	}


	// Use this for initialization
	void Start () {
		//initial settings of controls
		curvatureSliderValue = 25;
		hillsSliderValue = 5;
		trafficSliderValue = 10;
		spoilersSliderValue = 0;
		sidepropsSliderValue = 80;
	}
}
