using UnityEngine;
using System.Collections;

public class ElectricWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "player")
			GameController.DoGameOver ("You got electricuted!");
	}
}
