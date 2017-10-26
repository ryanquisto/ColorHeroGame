using UnityEngine;
using System.Collections;
//TODO: Raycast for collision with triggers
public class ChessPiece : MonoBehaviour {
    private Rigidbody rb;
    private const float MAX_VELOCITY = 20f;
    private const float MOVE_AMOUNT = 5f;
    private GameObject player;
    private bool CanMove;
    private bool RightOpen;
    private bool LeftOpen;
    private bool FrontOpen;
    private bool BackOpen;
    private bool MoveUp;
    private bool MoveBack;
    private bool MoveRight;
    private bool MoveLeft;
    private Vector3 StartPos;
    private bool InHole;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        CanMove = true;
        player = GameObject.Find("player");
		StartPos = transform.position;
		Invoke ("GetStartPos", 3);
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] FrontCollision = Physics.OverlapSphere(transform.position + new Vector3(0, 1.5f, 5), 2f);
        Collider[] BackCollision = Physics.OverlapSphere(transform.position + new Vector3(0, 1.5f, -5), 2f);
        Collider[] RightCollision = Physics.OverlapSphere(transform.position + new Vector3(5, 1.5f, 0), 2f);
        Collider[] LeftCollision = Physics.OverlapSphere(transform.position + new Vector3(-5, 1.5f, 0), 2f);

        FrontOpen = (FrontCollision.Length ==0);
        BackOpen = (BackCollision.Length ==0);
        RightOpen = (RightCollision.Length ==0);
        LeftOpen = (LeftCollision.Length ==0);

        if (player != null && CanMove)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 5 && Mathf.Abs(player.transform.position.z - transform.position.z) < 0.5f)
            {
                if (player.transform.position.x > transform.position.x && Input.GetKey(KeyCode.LeftArrow) && LeftOpen)
                    MoveLeft = true;
                else if (Input.GetKey(KeyCode.RightArrow) && RightOpen)
                    MoveRight = true;
                else
                    return;
                CanMove = false;
                Invoke("ResetMove", 1);
            }
            else if (Mathf.Abs(player.transform.position.z - transform.position.z) < 5 && Mathf.Abs(player.transform.position.x - transform.position.x) < 0.5f)
            {
                if (player.transform.position.z > transform.position.z && Input.GetKey(KeyCode.DownArrow) && BackOpen)
                    MoveBack = true;
                else if (Input.GetKey(KeyCode.UpArrow) && FrontOpen)
                    MoveUp = true;
                else
                    return;
                CanMove = false;
                Invoke("ResetMove", 1);
            }

        }

    }

    void LateUpdate()
    {
        if (MoveUp)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
            transform.position = Vector3.Slerp(transform.position, transform.position + new Vector3(0, 0, MOVE_AMOUNT), Time.deltaTime);
        }
        else if (MoveBack)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
            transform.position = Vector3.Slerp(transform.position, transform.position + new Vector3(0, 0, MOVE_AMOUNT * -1), Time.deltaTime);
        }
        else if (MoveRight)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            transform.position = Vector3.Slerp(transform.position, transform.position + new Vector3(MOVE_AMOUNT, 0, 0), Time.deltaTime);
        }
        else if (MoveLeft)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            transform.position = Vector3.Slerp(transform.position, transform.position + new Vector3(MOVE_AMOUNT * -1, 0, 0), Time.deltaTime);
        }
    }

    void ResetMove()
    {
        CanMove = true;
        MoveUp = false;
        MoveBack = false;
        MoveRight = false;
        MoveLeft = false;
		//InHole = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
		InvokeRepeating ("CheckGlitch", 0, 2);
    }

    public void ResetPos()
    {
        transform.position = StartPos;
    }

	public void setDown(){
		InHole = true;
	}

	void GetStartPos(){
		StartPos = transform.position;
	}

	void CheckGlitch(){
		if (InHole)
			CancelInvoke ();
		if (transform.position.y != StartPos.y && !InHole) {
			transform.position = new Vector3 (transform.position.x, StartPos.y, transform.position.z);
		}
	}

}
