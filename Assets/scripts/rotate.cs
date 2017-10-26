using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	Rigidbody rb;

	private float x;
	private float y;
	private float z;

	public int RotationSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		getRandomRotation ();
		InvokeRepeating ("getRandomRotation", 0, 5);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Time.time%5 == 0) {
			x = Random.Range (-1, 1);
			y = Random.Range (-1, 1);
			z = Random.Range (-1, 1);
		}
		rb.transform.Rotate (x, y, z);
	}

	Vector3 getRandomRotation()
	{
		x = Random.Range (-1, 1);
		y = Random.Range (-1, 1);
		z = Random.Range (-1, 1);
		if (Mathf.Abs (x) + Mathf.Abs (y) + Mathf.Abs (z) < 2)
			getRandomRotation ();
		return new Vector3 (x, y, z);
	}
}
