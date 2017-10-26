using UnityEngine;
using System.Collections;
using System;

public class player_control : MonoBehaviour {

	public float speed;
	private float originalSpeed;
    private Vector3 movement;
	private Rigidbody rb;
	private float NormalDrag;
	public float SlowingDrag;
	public static bool CanMove;
    public static bool CanJump;
	private bool Jumping;
	private bool CheckingForLanding;
    public static bool Slippery = false;
    private KeyCode Forward;
    public static bool NoSwapZone;
    private bool OnSurface;
	private const float JUMP_VALUE = 40f;
	private const float MAX_SPEED = 8f;

    void Start()
	{
		CanMove = true;
        CanJump = true;
		rb = GetComponent<Rigidbody>();
		NormalDrag = rb.drag;
        Forward = KeyCode.UpArrow;
        NoSwapZone = false;
		originalSpeed = speed;
	}

	void FixedUpdate()
	{
        //assuming forward is up arrow
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //check in case not true
        if (Forward != KeyCode.UpArrow)
        {
            if (Forward == KeyCode.DownArrow)
            {
                moveHorizontal = Input.GetAxis("Horizontal") * -1;
                moveVertical = Input.GetAxis("Vertical") * -1;
            }
            else if (Forward == KeyCode.RightArrow)
            {
                moveHorizontal = Input.GetAxis("Vertical") * -1;
                moveVertical = Input.GetAxis("Horizontal");
            }
            else if (Forward == KeyCode.LeftArrow)
            {
                moveHorizontal = Input.GetAxis("Vertical");
                moveVertical = Input.GetAxis("Horizontal") * -1;
            }
        }
		movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		if (CanMove) {
			rb.AddForce (movement * speed);
		}
			


        //Reduces the effort it takes to change directions, assuming not slippery
        if (!player_control.Slippery)
        {
            if (Math.Abs(rb.velocity.x + movement.x) < Math.Abs(rb.velocity.x))//player is trying to switch horizontal directions
            {
                //let's help them out by quickly cancelling out the opposing force
                rb.velocity = new Vector3(rb.velocity.x / 5, rb.velocity.y, rb.velocity.z);
            }
            if (Math.Abs(rb.velocity.z + movement.z) < Math.Abs(rb.velocity.z)) //player is trying to switch vertical directions
            {
            //let's help them out by quickly cancelling out the opposing force
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z / 5);
            }
        }
        else
            rb.drag = 0;

        

		//control stops the player
		if (Input.GetKey (KeyCode.LeftControl))
			rb.velocity = new Vector3(0,0,0);

		if (Jumping) {
			if (Mathf.Abs(rb.velocity.y) < 0.1f && !CheckingForLanding){
				CheckingForLanding = true;
				//to make sure player actually hit ground and is not at top of jump, wait 0.1 seconds and check speed again
				Invoke ("CheckIfActuallyLanded", 0.1f);
			}
		}
		//to jump, hit spacebar (if statement insures that player cannot jump more than once before hitting the ground)
		if (Input.GetKeyDown (KeyCode.Space) && !Jumping && CanJump) {
			DoJump(500);
		}



		//check if the player is falling and there is nothing beneath to catch it (if so, game over)
		if (rb.velocity.y < 0) {
			//try to correct the terribly slow falling gravity force
			//rb.AddForce(0, -12f, 0);
			if (Physics.RaycastAll (new Ray (transform.position, new Vector3 (0, -1, 0)), 100f).Length == 0 && !OnSurface)
				Invoke ("CheckGameOver", 1f);
                
		}
	}

	void CheckGameOver(){
		if (Physics.RaycastAll (new Ray (transform.position, Vector3.down), 100f).Length == 0 && !OnSurface && Physics.OverlapSphere(transform.position + 9*Vector3.down, 9).Length < 2)
			GameController.DoGameOver("You fell down!");
	}

    void OnCollisionEnter(Collision col)
    {
        OnSurface = true;
    }

	void OnCollisionStay(Collision col){
		OnSurface = true;
	}

    void OnCollisionExit(Collision col)
    {
        OnSurface = false;
    }

    void LateUpdate()
	{
		if (movement.x==0.0f && movement.z==0.0f && !player_control.Slippery) 
			rb.drag = SlowingDrag;
		else
			rb.drag = NormalDrag;
		if (rb.velocity.magnitude > MAX_SPEED && (OnSurface || Jumping))
			rb.velocity *= MAX_SPEED / rb.velocity.magnitude;
	}


	void CheckIfActuallyLanded(){
        var Collisions = Physics.OverlapSphere(transform.position, 1.3f);
        //if (Mathf.Abs (rb.velocity.y) < 0.1f) { 
        if (Collisions.Length> 1) { 
			Jumping = false;
		}
		CheckingForLanding = false;
	}

	public void DoJump(int Force){
		/**JumpInputReduction = InputReduction;
		rb.velocity = new Vector3(0,0,0);
		rb.AddForce (0f, Force * rb.mass, 0f);
		Jumping = true;
		speed /= InputReduction;*/
		//float velocityMag = rb.velocity.magnitude;
		//Vector3 newVelocity = rb.velocity + Vector3.up * JUMP_VALUE;
		/**if (velocityMag > 0) {
			float scale = velocityMag / newVelocity.magnitude;
			newVelocity *= scale;
		}*/
		//rb.velocity = newVelocity;
		rb.AddForce(Vector3.up * Force);
		Jumping = true;

	}

    public void ChangeArrowKeys(KeyCode WhichIsForward)
    {
        Forward = WhichIsForward;
    }

	public void setSpeed(float multiplier){
		speed *= multiplier;
	}

	public void resetSpeed(){
		speed = originalSpeed;
	}
}
