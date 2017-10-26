using UnityEngine;
using System.Collections;
using System;

public class geyser : MonoBehaviour {
	ParticleSystem water;
	private bool MovingUp;
	const int MAX_HEIGHT = 15;
	private GameObject player;
	private GameObject TopCollision;
	private Animator Wheel;
	private Animator GeyserBase;
	private int WheelForwardSpinHash;
	// Use this for initialization
	void Start () {
		water = GetComponent<ParticleSystem> ();
		water.Stop ();
		TopCollision = transform.GetChild (0).gameObject;
		Wheel = transform.Find ("geyser_wheel").GetComponent<Animator> ();
		GeyserBase = transform.Find ("geyser_base").GetComponent<Animator> ();
		WheelForwardSpinHash = Animator.StringToHash ("GeyserTrigger");
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			//geyser moves the player up, player unable to move
			if (MovingUp && player.transform.position.y - transform.position.y < MAX_HEIGHT - .1f) {
				player.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 6f, 0f);
				player_control.CanMove = false;
			} else {
				//geyser has reached the top, player now bounces up and down and can move again
				MovingUp = false;
                player_control.CanMove = true;

			}
		} 
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "player") {
			player = col.gameObject;
		}
	}


	void OnTriggerExit(Collider col)
	{
		player = null;
		water.Stop ();
		TopCollision.SetActive(false);
		Wheel.ResetTrigger ("GeyserTrigger");
		Wheel.SetBool ("PlayerOutOfRange", true);

	}

	void BeginWaterfallAscending(GameObject FoundPlayer)
	{
		Wheel.SetTrigger(WheelForwardSpinHash);
		water.Play ();
		MovingUp = true;
		SphereCollider sphere = GetComponentInChildren<SphereCollider> ();
		player = FoundPlayer;

		//snap player to center of geyser
		player.transform.position = sphere.transform.position;

		TopCollision.SetActive (true);

		Wheel.SetTrigger (WheelForwardSpinHash);
	}
}
