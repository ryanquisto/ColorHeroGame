using UnityEngine;
using System.Collections;
using System;

public class SettingsPanel : ClosableUIElement {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Close()
    {
        this.gameObject.SetActive(false);
    }

}
