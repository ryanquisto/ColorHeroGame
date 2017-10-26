using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PixieDust : MonoBehaviour {
	public const int PixieDustLifetime = 60;
	private bool FollowingPlayer;
	private GameObject player;
	private float TimeRemaining;
	private Text TimerText;
	public static bool HasPixieDust = false;
	// Use this for initialization
	void Start () {
		TimeRemaining = PixieDustLifetime;
		TimerText = GameObject.Find ("PixieTimeText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (FollowingPlayer) {
			transform.position = player.transform.position;
			TimeRemaining-=Time.deltaTime;
			TimerText.text = Convert.ToString((int) TimeRemaining);
		}
		if (TimeRemaining <= 0) {
			GameController.DoGameOver ("Your pixie dust lost its magic!");
			PixieDust.HasPixieDust = false;
			Destroy(this.gameObject);
		}

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "player") {
			GameController.ShowTutorialTip ("PixieDust", "You just found the pixie dust. Along with the wrench, you'll be able to repair this level's machine. Be careful though - monitor the time left before you run out at the top.", new Vector3 ((TimerText.transform.position.x-100) / Screen.width, TimerText.transform.position.y/Screen.height - 0.25f, 0f), "PixieDustInitialCollect", true);
			FollowingPlayer = true;
			PixieDust.HasPixieDust = true;
			transform.FindChild("PixieCollect").GetComponent<ParticleSystem>().Play();
			player = col.gameObject;
			TimerText.gameObject.SendMessageUpwards("UpdatePixieDustImage");
			GetComponent<SphereCollider>().enabled = false;
		}

	}
}
