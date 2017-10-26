using UnityEngine;
using System.Collections;

public class AdvanceLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		//if (GameController.HasWrench && GameController.HasPixieDust){
		if (!PixieDust.HasPixieDust && !GameController.HasWrench)
			GameController.ShowTutorialTip ("Whoops!", "You'll need a wrench and pixie dust to repair the machine.", new Vector3 (0.3f, 0.6f, 06f), "", true);
		else if (! PixieDust.HasPixieDust) 
			GameController.ShowTutorialTip ("Whoops!", "You don't have pixie dust!", new Vector3 (0.3f, 0.6f, 0.6f), "", true);
		else if (!GameController.HasWrench)
			GameController.ShowTutorialTip ("Whoops!", "You don't have a wrench!", new Vector3 (0.3f, 0.6f, 0.6f), "", true);
		else 
			GameController.AdvanceLevel ();
		//stuff to add points for light cubes
	}
}
