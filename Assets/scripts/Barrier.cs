using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
    private bool MovingUp;
    private bool MovingDown;
    private Vector3 UpPos;
    private Vector3 DownPos;
    private GameObject Spikes;
    private MeshCollider collision;
    private float StartTime;
	// Use this for initialization
	void Start () {
        Spikes = transform.Find("Spikes").gameObject;
        UpPos = Spikes.transform.position;
        DownPos = Spikes.transform.position + new Vector3(0,-4.5f);
        collision = Spikes.GetComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
	if (MovingUp)
        {
            Spikes.transform.position = Vector3.Slerp(DownPos, UpPos, Time.time - StartTime);
            Invoke("StopMoving", 1f);
        }
    else if (MovingDown)
        {
            Spikes.transform.position = Vector3.Slerp(UpPos, DownPos, Time.time - StartTime);
            Invoke("StopMoving", 1f);
        }
	}

    public void Unlock()
    {
        MovingDown = true;
        collision.enabled = false;
        StartTime = Time.time;
    }
    public void Lock()
    {
        MovingUp = true;
        collision.enabled = true;
        StartTime = Time.time;
    }

    void StopMoving()
    {
        MovingUp = false;
        MovingDown = false;
    }
}
