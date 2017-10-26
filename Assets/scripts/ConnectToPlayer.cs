using UnityEngine;
using System.Collections;

public class ConnectToPlayer : MonoBehaviour {
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + new Vector3(5, 5, 0);
	}
}
