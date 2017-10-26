using UnityEngine;
using System.Collections;

public class LightCubeCollect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){

		//if (col.gameObject.name == "player") {
		GameController.ShowTutorialTip ("Light Cube", "You just got a light cube! Collect as many as you can to restore lost light at the end of the level.", new Vector3(0.5f, 0.6f, 0.0f), "1-02");	
		SendMessageUpwards("BeginCollecting", col.gameObject, SendMessageOptions.RequireReceiver);
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
		//}
	}
}
