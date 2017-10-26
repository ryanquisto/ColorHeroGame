using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour {
	private Slider slider;
	GameController control;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		slider.onValueChanged.AddListener (delegate {
			ValueChangeCheck ();
		});
		control = GameObject.Find("_GameController").GetComponent<GameController>();
		Invoke("SetInitialVolume", 0.1f);		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ValueChangeCheck(){
		if (gameObject.name == "MusicVolumeSlider") {
			control.SetMusicVolume (GetComponent<Slider>().value);
		}

	}

	void SetInitialVolume(){
		slider.value = GameObject.Find ("GameMusic").GetComponent<AudioSource> ().volume;

	}

}
