using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectToggle : MonoBehaviour {
	public Sprite ToggleOn;
	public Sprite ToggleOff;
	private bool EffectsOn = true;
	private Button Checkbox;
	private Slider slider;
	// Use this for initialization
	void Start () {
		Checkbox = GetComponent<Button> ();
		slider = GameObject.Find("EffectVolumeSlider").GetComponent<Slider>();
		if (GameController.EffectsEnabled) {
			EffectsOn = true;
			Checkbox.GetComponent<Image> ().sprite = ToggleOn;
			slider.interactable = true;
		} else {
			EffectsOn = false;
			Checkbox.GetComponent<Image>().sprite = ToggleOff;
			slider.interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnClick(){
		EffectsOn = !EffectsOn;
		if (EffectsOn){
			Checkbox.GetComponent<Image>().sprite = ToggleOn;
			GameObject.Find ("_GameController").GetComponent<GameController>().ToggleAudio("Effects");
			slider.interactable = true;
		}
		else{
			Checkbox.GetComponent<Image>().sprite = ToggleOff;
			GameObject.Find ("_GameController").GetComponent<GameController>().ToggleAudio("Effects");
			slider.interactable = false;
		}
	}
}