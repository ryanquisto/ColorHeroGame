using UnityEngine;
using System.Collections;

public class MazeSpikeTrigger : MonoBehaviour {
    private Barrier Barrier;
    private bool Activated;
	// Use this for initialization
	void Start () {
        Barrier = transform.Find("maze_barrier").GetComponent<Barrier>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("piece") && !Activated)
        {
			col.gameObject.GetComponent<ChessPiece> ().setDown ();
            Barrier.Unlock();
            Activated = true;
        }

    }

    public void Reset()
    {
        Activated = false;
        Barrier.Lock();
    }
}
