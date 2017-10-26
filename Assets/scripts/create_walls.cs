using UnityEngine;
using System.Collections;

public class create_walls : MonoBehaviour {
	public GameObject Wall;
	private float x;
	private float y;
	private float z;
    private bool IsIce = false;
	// Use this for initialization
	void Start () {
        if (gameObject.name.Contains("ice"))
            IsIce = true;
		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;
        if (Wall != null)
        {
            //check area to the left to decide if a wall should be created
            var OnLeft = Physics.OverlapSphere(new Vector3(x - 10f, y + 4f, z), 2.5f);//1 is purely chosen arbitrarly
            if (OnLeft.Length == 0) //no collisions on this side
                Instantiate(Wall, new Vector3(x - 4.5f*transform.localScale.x, y + 5.99f, z), new Quaternion(90f, 0f, 90f, 0f));

            //check to the right
            var OnRight = Physics.OverlapSphere(new Vector3(x + 10f, y + 4f, z), 2.5f);//1 is purely chosen arbitrarly
            if (OnRight.Length == 0) //no collisions on this side
                Instantiate(Wall, new Vector3(x + 4.5f*transform.localScale.x, y + 5.99f, z), new Quaternion(90f, 0f, 90f, 0f));

            //check in front
            var OnFront = Physics.OverlapSphere(new Vector3(x, y + 4f, z + 10f), 2.5f);//1 is purely chosen arbitrarly
            if (OnFront.Length == 0) //no collisions on this side
                Instantiate(Wall, new Vector3(x, y + 6f, z + 4.5f*transform.localScale.z), new Quaternion(0f, 0f, 90f, 0f));

            //check behind
            var OnBack = Physics.OverlapSphere(new Vector3(x, y + 4f, z - 10f), 2.5f);//1 is purely chosen arbitrarly
            if (OnBack.Length == 0) //no collisions on this side
                Instantiate(Wall, new Vector3(x, y + 6f, z - 4.5f * transform.localScale.z), new Quaternion(90f, 0f, 0f, 0f));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        if (IsIce && col.gameObject.name == "player")
        {
            player_control.Slippery = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (IsIce && col.gameObject.name == "player")
        {
            player_control.Slippery = false;
        }
    }
}
