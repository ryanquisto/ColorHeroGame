using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CheatCodes : ClosableUIElement {
	GameObject obj;
	InputField input;
	string paramValue;
	player_control player;
	// Use this for initialization
	void Start () {
		obj = transform.FindChild("CodeInput").gameObject;
		input = transform.GetComponentInChildren<InputField> ();
		obj.SetActive (false);
		player = GameObject.Find ("player").GetComponent<player_control>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			if (obj.gameObject.activeSelf) {
				player_control.CanJump = true;
				player_control.CanMove = true;
				if (!input.text.Equals ("")) {
					Invoke (InterpretInput (input.text), 0.1f);
					Invoke ("ResetParamVal", 2f);
				}
				Close ();
			}
			else {
				obj.gameObject.SetActive (true);
				EventSystem.current.SetSelectedGameObject (obj, null);
				player_control.CanJump = false;
				player_control.CanMove = false;

			}
		}
	}

	public override void Close(){
		input.text = "";
		obj.SetActive (false);
	}

	String InterpretInput(string info){
		if (info == null || info.Equals (""))
			return null;
		string methodName;
		//char[] chars = info.ToCharArray ();
		//int endValue = 0;
		int multiplier = 1;
		if (info.Contains (" ")) {
			methodName = info.Substring (0, info.IndexOf (' ')).Trim ();
			paramValue = info.Substring (info.IndexOf (' ') + 1);
		} else
			methodName = info;
		/**for (int i = chars.Length - 1; i > 0; i--) {
			if (Char.IsNumber (chars [i])) {
				endValue += (chars [i] - '0') * multiplier;
				multiplier *= 10;
				methodName = methodName.Remove (i, 1);
			} else {
				paramValue = endValue;
				break;
			}
		}
		methodName = methodName.Trim ();*/
		//Messenger.SendMessage (methodName + " param " + paramValue);
		return methodName;
			
	}

	void ResetParamVal(){
		paramValue = "";
	}

	/**Adds time to level*/
	void time(){
		GameObject.Find ("LevelLight").GetComponent<LightTimer> ().AddTime (Convert.ToInt32(paramValue));
	}

	void die(){
		GameController.DoGameOver (paramValue);
	}

	void gimme(){
		GameObject obj = new GameObject ();
		switch (paramValue) {
		case "timepotion":
			obj.AddComponent<TimePotion> ();
			break;
		case "puzzlepotion":
			obj.AddComponent<RedoPotion> ();
			break;
		case "sun":
			obj.AddComponent<Sun> ();
			break;
		case "magnet":
			obj.AddComponent<Magnet> ();
			break;
		default:
			Messenger.SendMessage("Not a recognized item");
			break;

		}
		obj.GetComponent<Item> ().CheckForCollect(true);


	}
		
	void print(){
		Messenger.SendMessage (paramValue);
	}

	void teleport(){
		Vector3 position;
		string remaining = paramValue;
		position.x = Convert.ToInt32 (remaining.Substring (0, remaining.IndexOf(',')));
		remaining = remaining.Remove (0, remaining.IndexOf (',')+1);
		position.y = Convert.ToInt32(remaining.Substring(0, remaining.IndexOf(',')));
		remaining = remaining.Remove (0, remaining.IndexOf (',')+1);
		position.z = Convert.ToInt32(remaining);
		player.gameObject.transform.position = position;
	}

	/**Teleport player above the furthest found object with given tag*/
	void moveto(){
		GameObject[] possibleObjects = GameObject.FindGameObjectsWithTag (paramValue);
		if (possibleObjects.Length == 0) {
			Messenger.SendMessage ("No objects found by that name");
			return;
		}
		float distance = Vector3.Magnitude(possibleObjects [0].transform.position - player.transform.position);
		GameObject result = possibleObjects[0];
		foreach(GameObject o in possibleObjects){
			float thisDistance = Vector3.Magnitude (o.transform.position - player.transform.position);
			if (thisDistance > distance) {
				distance = thisDistance;
				result = o;
			}
		}
		player.gameObject.transform.position = result.transform.position + 10*Vector3.up;
	}

	void timescale(){
		Time.timeScale = Convert.ToInt32 (paramValue);
		player.setSpeed (1.5f / Convert.ToInt32 (paramValue));
	}

	void printlocation(){
		Vector3 position = player.gameObject.transform.position;
		Messenger.SendMessage ("Current position: " + position.ToString ()); 
	}
}
