using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WrenchIcon : MonoBehaviour {
	public Sprite CollectedImage;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SwitchToCollectedImage(){
		GetComponent<Image> ().sprite = CollectedImage;
	}
}
