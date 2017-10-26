using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {
	private AudioSource music;
	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource> ();
	if (! GameController.PlayMusic)
			music.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		if (music.isPlaying && !GameController.PlayMusic)
			music.Stop ();
		else if (!music.isPlaying && GameController.PlayMusic)
			music.Play ();
	
	}
}
