using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CubeMovement : MonoBehaviour {
	private bool Frantic;
	private float MovementTime;
	private Vector3 StepMovement;
	private GameObject player;
	private Vector3 Force;
	private Rigidbody rb;
	private bool collecting;
	private float HoverCount;
	private bool StartScaling;
	private Vector3 InitialPosition;
	private bool Destroying;
	private float Difficulty;
	private AudioSource collectfx;
	// Use this for initialization
	void Start () {
		Frantic = false;
		rb = GetComponent<Rigidbody> ();
		InitialPosition = transform.position;
		collectfx = GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		//attempt to stop problem of cube flying too high to catch:
		if (player !=null && transform.position.y - 0.2 > player.transform.position.y) {
			rb.AddForce (0, -1 * Mathf.Pow (rb.velocity.y, 2f), 0);
		}

        if (rb.velocity.magnitude > Difficulty)
            rb.AddForce(rb.velocity * -1);


		if (Frantic && !collecting) {
			//new movement, decide what force to add
			if (player != null && !collecting && MovementTime < 0.1){
				Force = (transform.position - player.transform.position) * Random.Range (1, 3) * Difficulty * 50*Time.deltaTime * (10/Mathf.Pow (Vector3.Magnitude(transform.position - player.transform.position),2f));
				Force.y = 0;
				rb.AddForce (Force);

			} else if (MovementTime > 1)	//stops for 1 more second, then gets a new force
				MovementTime = 0;
			else {
				MovementTime += Time.deltaTime;
			}

			//MovementTime += Time.deltaTime;
		}

		//Animation to collect the cube
		if ((collecting && player != null) || Destroying) {
			if (!Destroying){
			Frantic = false;
			Vector3 AbovePlayer = player.transform.position + new Vector3 (0f, player.transform.localScale.y + 1.5f, 0f);
			transform.position = Vector3.MoveTowards (transform.position, AbovePlayer, 8 * Time.deltaTime);

			if (Vector3.Magnitude (transform.position - AbovePlayer) < 0.2){	//once the cube is close enough to desired position, it starts shrinking
				StartScaling = true;
				Invoke("PlayEffect", 0.6f);
				}
			}

			if (StartScaling || Destroying) {
				transform.localScale *= 0.95f;
				if (transform.localScale.x < 0.4f){ 	//once it's small enough, it finally disappears
					Destroy (this.gameObject);
					if (!Destroying)
						GameController.ShowTutorialTip ("Light Cube", "You just got a light cube! Collect as many as you can to restore lost light at the end of the level.", new Vector3(0.5f, 0.6f, 0.0f), "LightCube");	
					else 
						GameController.ShowTutorialTip ("Light Cube", "Uh oh - The light cube got away! If you manage to catch one later on, they will help you restore lost light at the end of the level.", new Vector3(0.5f, 0.6f, 0.0f), "LightCube");  
					GameObject.Find ("LightCubeCount").GetComponent<Text>().text = System.Convert.ToString(GameController.LightCubeCount);

				}
			}
		}

		if (transform.position.y - InitialPosition.y > 1)
			rb.AddForce (0, rb.velocity.y * rb.mass, 0);

		if (Vector3.Magnitude (transform.position - InitialPosition) > 10 && !Frantic)
			Invoke ("DestroyCube", 5);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "player"){
			Frantic = true;
			player = col.gameObject;
		}
	}

	void OnTriggerExit(Collider col){
		Frantic = false;
		if(!collecting)
			player = null;
	}

	void OnCollisionEnter(Collision col){	
		if (col.gameObject.name == "player") {	//player got the cube
			player = col.gameObject;
			GameController.LightCubeCount++;
			collecting = true;
			Frantic = false;
			rb.velocity = new Vector3(0,0,0);
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
			collectfx.Play ();
		}
	}

	void DestroyCube(){
		Destroying = true;
	}

	public void SetDifficulty(int difficulty){
		Difficulty = difficulty;
	}
	
}
