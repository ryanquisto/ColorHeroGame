using UnityEngine;
using System.Collections;

public class LightningBarrier : MonoBehaviour {
	public float ActivationTime;
	private ParticleSystem Lightning1;
	private ParticleSystem Lightning2;
	private BoxCollider Trigger;
	private float Timer;
	private bool ShouldUpdate;
	// Use this for initialization
	void Start () {
		Lightning1 = transform.Find ("Lightning1").GetComponent<ParticleSystem>();
		Lightning2 = transform.Find ("Lightning2").GetComponent<ParticleSystem>();
		Trigger = GetComponent<BoxCollider> ();
		Invoke ("DisableLightning", 0.1f);
		ShouldUpdate = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (ShouldUpdate)
			Timer += Time.deltaTime;
		if (Timer >= ActivationTime) {
			LightningStartUp();
			ShouldUpdate = false;
			Timer = 0;
			}


	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "player")
			GameController.DoGameOver ("You got electricuted!");
	}

	void DisableLightning(){
		Lightning1.Stop ();
		Lightning2.Stop ();
		Trigger.enabled = false;
	}

	void LightningStartUp(){
		Invoke ("LightningEnable",2);
		Lightning1.emissionRate = 1;
		Lightning2.emissionRate = 1;
		Lightning1.Play ();
		Lightning2.Play ();
	}

	void LightningEnable(){
		Trigger.enabled = true;
		Lightning1.emissionRate = 8;
		Lightning2.emissionRate = 8;
	}

	public void Reset(){
		DisableLightning ();
		Timer = 0;
		ShouldUpdate = true;
	}

}
