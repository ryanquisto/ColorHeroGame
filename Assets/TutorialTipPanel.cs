using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TutorialTipPanel : ClosableUIElement {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Close()
    {
		GameObject.Find ("TutorialTip").transform.Find ("ExitButton").GetComponent<Button> ().onClick.Invoke();
        //gameObject.SetActive(false);
    }
}
