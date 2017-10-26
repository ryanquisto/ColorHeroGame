using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour {
	public Sprite ToggleOn;
	public Sprite ToggleOff;
	private bool MusicOn;
	private Button Checkbox;
	private GameController control;
	private Slider slider;
	// Use this for initialization
	void Start () {
		control = GameObject.Find ("_GameController").GetComponent<GameController> ();
		slider = GameObject.Find("MusicVolumeSlider").GetComponent<Slider>();
		Checkbox = GetComponent<Button> ();
		if (GameController.MusicEnabled) {
			MusicOn = true;
			Checkbox.GetComponent<Image> ().sprite = ToggleOn;
			slider.interactable = true;
		} else {
			MusicOn = false;
			Checkbox.GetComponent<Image>().sprite = ToggleOff;
			slider.interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick(){
		MusicOn = !MusicOn;
		if (MusicOn){
			Checkbox.GetComponent<Image>().sprite = ToggleOn;
			slider.interactable = true;
		}
		else{
			Checkbox.GetComponent<Image>().sprite = ToggleOff;
			slider.interactable = false;
		}
		control.ToggleAudio("Music");
	}
	
}
