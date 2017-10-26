using UnityEngine;
using System.Collections;

public class LightningButtonPress : MonoBehaviour {
	public GameObject Barrier;
	private bool PressingDown;
	private bool PullingUp;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		if (PressingDown) {
			if (transform.localPosition.y <= 0.0f){
				PressingDown = false;
				PullingUp  = true;
			}
			else 
				transform.Translate (0f, -0.05f, 0f);
		} else if (PullingUp) {
			if (transform.localPosition.y >= 0.2f)
				PullingUp = false;
			else
				transform.Translate (0f, 0.02f, 0f);
		}
	}

	void OnTriggerEnter(){
		if (!PressingDown) {
			Barrier.GetComponent<LightningBarrier> ().Reset ();
			PressingDown = true;
		}
	}
    void OnTriggerStay()
    {
        Barrier.GetComponent<LightningBarrier>().Reset();
        PressingDown = true;
    }
}
