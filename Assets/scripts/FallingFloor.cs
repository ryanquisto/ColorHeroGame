using UnityEngine;
using System.Collections;

public class FallingFloor : MonoBehaviour {
    private Vector3 StartPosition;
    private bool StartFalling;
    private float FallingTime;
	// Use this for initialization
	void Start () {
        StartPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
        if (StartFalling)
        {
            //transform.Translate(0, 0, -2 * Time.deltaTime);
            transform.position = Vector3.Slerp(transform.position, StartPosition + new Vector3(0, -5, 0), Time.deltaTime/4);
            FallingTime += Time.deltaTime;
            VisualEffects.FadeObject(this.gameObject);
            if (FallingTime >= 7)
                Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            StartFalling = true;
        }
    }
}
