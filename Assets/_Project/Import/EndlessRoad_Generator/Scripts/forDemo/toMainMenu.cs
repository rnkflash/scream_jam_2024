//written by Boris Chuprin smokerr@mail.ru
//this script is for 'Escape' button to load Main Menu scene from any other demo scene
//written for demo

using UnityEngine;
using System.Collections;

public class toMainMenu : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if  (Input.GetKeyDown(KeyCode.Escape)){
			gameObject.AddComponent<fadeIn>();
			StartCoroutine(sceneLoader("demo_Select"));
		}
	}

	private IEnumerator sceneLoader(string sceneName) {
		yield return new WaitForSeconds(1);
		Application.LoadLevel(sceneName);
	}
}
