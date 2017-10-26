using UnityEngine;
using System.Collections;
using System;

public class trampoline : MonoBehaviour {
	private SphereCollider JumpTrigger;
	private bool WatchForJump;
	private float DistanceFromCenter;
	private GameObject player;
    private bool Jumping;
    private float DownVelocity;
    private float JumpHeight;
	// Use this for initialization
	void Start () {
		JumpTrigger = GetComponent<SphereCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
            return;
   //     if (WatchForJump && player != null)
   //     {
   //         DistanceFromCenter = Vector3.Magnitude(player.transform.position - JumpTrigger.center);
   //         if (Input.GetKeyDown(KeyCode.Space) && player_control.CanMove)
   //         {
   //             player.GetComponent<Rigidbody>().velocity = Vector3.zero;
   //             Invoke("AddJumpForce", 0.05f);
   //             Jumping = true;
   //             player_control.CanMove = false;
   //             return;
   //         }
   //     }
			//if (Jumping && player.GetComponent<Rigidbody>().velocity.y < 0)
   //     {
			//	player_control.CanMove = true;
			//	player = null;
   //             Jumping = false;
			//}
        if (player.transform.position.y - transform.position.y > JumpHeight * .8)  //approaching max height
        {

            player.GetComponent<Rigidbody>().AddForce(0, player.GetComponent<Rigidbody>().velocity.y * -1, 0);
            player_control.CanMove = true;
        }
            if (Jumping)
        {
            Collider[] PlayerCollision = Physics.OverlapSphere(player.transform.position, 2.5f);
            if (Mathf.Abs(player.GetComponent<Rigidbody>().velocity.y) < 0.1 && PlayerCollision.Length ==0)
                JumpHeight = (player.transform.position.y - transform.position.y) * 1.2f;
            if (PlayerCollision.Length > 0)
            {
                for (int i = 0; i < PlayerCollision.Length; i++)
                {
                    if (PlayerCollision[i].gameObject.name.Contains("trampoline"))
                        break;
                    if (i == PlayerCollision.Length - 1)
                        ForgetPlayer();
                }
            }
        }

	}

	void OnTriggerEnter(Collider col){
        if (col.gameObject.name.Equals("player"))
        {
            //       {
            //		WatchForJump = true;
            if (player == null)
            {   //First jump
                player = col.gameObject;
                JumpHeight = 100;
            }
            else
                player = col.gameObject;
            //           player_control.CanJump = false;

            //       }
            //}
            //   void OnTriggerStay(Collider col)
            //   {
            //       if (player == null && col.gameObject.name.Equals("player"))
            //       {
            //           WatchForJump = true;
            //           player = col.gameObject;
            //           player_control.CanJump = false;
            //       }
        }
    }

	//void OnTriggerExit(Collider col){
		//if (col.gameObject.name.Equals("player")) {
	//		WatchForJump = false;
 //           player_control.CanJump = true;
		//}
	//}

    void OnCollisionEnter(Collision col){
        if (col.gameObject.name.Equals("player"))
        {
            if (Input.GetKey(KeyCode.Space))
                JumpHeight *= 2;
            col.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, JumpHeight, 0),transform.position + 2*(player.transform.position - transform.position));
            //JumpHeight = DownVelocity * -1000;
            player_control.CanMove = false;
            Jumping = true;
        }
    }

	//void AddJumpForce(){
 //       player.GetComponent<Rigidbody>().velocity = Vector3.zero;
 //       player.GetComponent<Rigidbody>().AddForce(0f, Convert.ToInt32(Mathf.Abs (10*Mathf.Pow(JumpTrigger.radius - DistanceFromCenter, 2))),0f);
 //       //player.GetComponent<player_control>().DoJump(Convert.ToInt32(Mathf.Abs(10 * Mathf.Pow(JumpTrigger.radius - DistanceFromCenter, 2))), 1f);
 //       player_control.CanJump = true;
	//}
    void ForgetPlayer()
    {
        player_control.CanMove = true;
        player = null;
        JumpHeight = 0;
        DownVelocity = 0;
        Jumping = false;
    }
}
