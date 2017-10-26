using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {
	private float time;
	private Image gear;
	private GameObject text;
	private string hover;
	private bool ShouldRotate;
	private const float GearScaleFactor = 0.7f;
	GameObject SettingsPanel;
	// Use this for initialization
	void Start () {
		gear = GetComponent<Image> ();
		text = GameObject.Find ("SettingsText");
		SettingsPanel = GameObject.Find ("SettingsPanel");
		SettingsPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		//performs a full rotation in 1 second every 3 seconds 
		if (ShouldRotate) {
			if (time > 1f && time < 3f)
				gear.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
			if (time > 3f) {
				time = 0f;
			} else if (time < 1f) {
				gear.transform.Rotate (0, 0, Time.deltaTime * 360);
			}
			time += Time.deltaTime;
		}



		//The following deals with the translation and scale of the button based on whether or not it's being hovered over
		if (Screen.height - Input.mousePosition.y < 128 && Input.mousePosition.x < 128 && !SettingsPanel.activeSelf) {	//mouse is hovering over the region

			if (Input.GetMouseButtonDown(0)){		//Clicked the button, open the panel
				SettingsPanel.SetActive(true);
			}

			//bring the button out
			if (GameObject.Find("Settings").transform.position.x < 0){
				GameObject.Find("Settings").transform.Translate(new Vector3(150*Time.deltaTime, 0f, 0f));
			}

			//text should be visible and button should spin now once button is out
			else{
				text.SetActive (true);
				ShouldRotate = true;
			}
		} else {	//mouse is not hovering over area
			//move back into the corner
			if (GameObject.Find("Settings").transform.position.x > -64*GearScaleFactor){
				GameObject.Find("Settings").transform.Translate(new Vector3(-150*Time.deltaTime, 0f, 0f));
			}
			//while it is in the corner, it should remain still and text should be hidden
			if(time > 1 || time == 0){	//stop spinning once the current spin is complete, if currently spinning
				ShouldRotate = false;
				time = 0;
				gear.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
			}
			text.SetActive (false);
		}

		//the gear's size is a function of it's position: 0.7 when in corner and 1.0 when showing
		float Scale = ((1-GearScaleFactor)/(64*GearScaleFactor))*GameObject.Find("Settings").transform.position.x +1;
		GameObject.Find("Settings").transform.localScale = new Vector3(Scale, Scale, Scale);
	}


}
