using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PixieIcon : MonoBehaviour {
	public Sprite EnabledImage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdatePixieDustImage(){
		GetComponent<Image> ().sprite = EnabledImage;
	}
}
