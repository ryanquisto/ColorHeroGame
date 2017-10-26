using UnityEngine;
using System.Collections;

public class teleporter : MonoBehaviour {
	private Transform TargetLocation;
	private ParticleSystem Swirls;
	private float AnimationTime;
	private GameObject player;
	private AudioSource sfx;
	// Use this for initialization
	void Start () {
		Swirls = transform.Find ("swirl").GetComponent<ParticleSystem>();
		Swirls.Stop ();
		TargetLocation = transform.Find ("TargetLocation");
		sfx = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null)
			player.transform.position = transform.position;
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "player"){
			player = col.gameObject;
			player.GetComponent<Rigidbody>().velocity = Vector3.zero;
			player.transform.position = transform.position;// + new Vector3(0f, transform.localScale.y * 1.325, 0f);
			Swirls.Play();
			Invoke ("TeleportPlayer", Swirls.duration+2);
			player_control.CanMove = false;
			sfx.Play ();
			//AudioSource.PlayClipAtPoint(sfx.clip, transform.position);
		}
	}

	void TeleportPlayer(){
		player.transform.position = TargetLocation.transform.position;// + new Vector3 (0f, player.transform.localScale, 0f);
		player_control.CanMove = true;
		player = null;
	}

}
