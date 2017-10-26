using UnityEngine;
using System.Collections;
using System.IO;

public class Tutorial : MonoBehaviour {
	public string Header;
	public string Text;
	public Vector3 Location;
	public bool PausesGame = false;
	public string ID;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "player") {
			//EVENTUALLY: Save whether or not player has already seen tip. This was working in editor, but when building, the text files weren't copied and weren't found
			//string text = File.ReadAllText (Application.dataPath + "/Preferences/TutorialHistory.txt");
			//if (! text.Contains (Header)) {
			GameController.ShowTutorialTip (Header, Text, Location, ID, PausesGame);
			//File.AppendAllText (Application.dataPath + "/Preferences/TutorialHistory.txt", Header);
			//}
			Destroy (this.gameObject);
		}
	}


}
