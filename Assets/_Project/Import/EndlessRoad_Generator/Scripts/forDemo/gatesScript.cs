//written by Boris Chuprin smokerr@mail.ru
//this script decides when player passsed thought this gates and finishes distance
//written for demo

using UnityEngine;
using System.Collections;

public class gatesScript : MonoBehaviour {

	GameObject linkToScenario;
	
	// Use this for initialization
	void Start () {
		linkToScenario = GameObject.Find ("gameScenario");
	}

	//when player passes this gates
	void OnTriggerEnter (Collider col)
	{
		if(col.transform.root.name == "player_vehicle")
		{
			if (linkToScenario.GetComponent<timerCountDown> ().endStatus == false){
				linkToScenario.GetComponent<timerCountDown> ().lostStatus = "WIN !";
				linkToScenario.GetComponent<timerCountDown> ().endStatus = true;
				StartCoroutine (loadScene (5));//call loading main menu in 5 seconds
			} else {
				return;
			}
		}
	}

	private IEnumerator loadScene (float secToWait){
		yield return new WaitForSeconds (secToWait-1);
		gameObject.AddComponent<fadeIn>();//call 'fading in' screen
		yield return new WaitForSeconds (1);
		Application.LoadLevel("demo_Select");
	}
}
