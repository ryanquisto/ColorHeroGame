using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wrench : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null){
			if (Input.GetKeyDown(KeyCode.X)){
				GameController.CollectPanel.SetActive(false);
				GameController.HasWrench = true;
				GameObject.Find("WrenchUI").GetComponent<WrenchIcon>().SwitchToCollectedImage();
				GameController.ShowTutorialTip("Wrench", "You found the wrench! Along with the pixie dust, you'll be able to restore the machine at the end of the level.", new Vector3(0.5f, 0.5f, 0.5f), "WrenchFound", true);
				Destroy(this.gameObject);
			}
		}

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name.Equals ("player")) {
			player = col.gameObject;
			GameController.CollectPanel.SetActive(true);
		}
	}

	void OnTriggerExit (Collider col){
        if (col.gameObject.name.Equals("player"))
        {
            player = null;
            GameController.CollectPanel.SetActive(false);
        }
	}
}
