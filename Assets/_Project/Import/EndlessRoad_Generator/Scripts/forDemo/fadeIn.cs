//written by Boris Chuprin smokerr@mail.ru
//this script fades in black screen
//written for demo

using UnityEngine;
using System.Collections;

public class fadeIn : MonoBehaviour {
	
	private Texture2D fadeTexture;
	private float fadeSpeed = 0.8f;
	private int drawDepth = -1000;

	private float alpha = 0.0f;
	private int fadeDir = 1;//-1 for fadeOut

	void OnGUI(){
		fadeTexture = Resources.Load("Textures/black_texture") as Texture2D;
		alpha += fadeDir * fadeSpeed * Time.deltaTime;  
		alpha = Mathf.Clamp01(alpha);
		GUI.color = new Color() {a = alpha};
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
	}
}
