using UnityEngine;
using System.Collections;

public class MazeLightProbe : MonoBehaviour {
    private ParticleSystem particles;
    public string color;
    public bool Solved;
	// Use this for initialization
	void Start () {
        particles = transform.Find("Particle System").gameObject.GetComponent<ParticleSystem>();
        particles.Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains(color) && col.gameObject.GetComponent<ChessPiece>() != null)
        {
            particles.Play();
            Solved = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name.Contains(color) && col.gameObject.GetComponent<ChessPiece>() != null)
        {
            Reset();
        }
    }

    public void Reset()
    {
        particles.Stop();
        Solved = false;
    }


}
