using UnityEngine;
using System.Collections;

public class geyser_bottom_trigger : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "player") {
			SendMessageUpwards("BeginWaterfallAscending", col.gameObject, SendMessageOptions.RequireReceiver);
		}
	}
}
