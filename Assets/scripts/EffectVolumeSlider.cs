using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectVolumeSlider : MonoBehaviour {
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
		if (gameObject.name == "EffectVolumeSlider") {
			control.SetEffectVolume (GetComponent<Slider>().value);
		}
		
	}
	
	void SetInitialVolume(){
		slider.value = GameController.EffectVolume;
		
	}

}
