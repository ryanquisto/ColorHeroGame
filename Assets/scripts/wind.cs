using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wind : MonoBehaviour {

	//public Vector3 direction;
	public bool WantArrowHelp;
	public float DirectionDuration;
	private bool InWindRegion;
	private GameObject player;
	public float MaxSpeedThroughWind;
	private Image arrow;
	public enum WindOptions
	{
		UpAndDown,
		ForwardAndBack,
		LeftAndRight
	};
	public WindOptions WindDirection;
	public int WindStrength;
	private Vector3 direction;
	const float ARROW_R = 119 / 255;
	const float ARROW_G = 255 / 255;
	const float ARROW_B = 120 / 255;
	const float ARROW_A = 255 / 255;
	private float[] ArrowColor = new float[4] {ARROW_R, ARROW_G, ARROW_B, ARROW_A};
	private ParticleSystem WindParticles;
	private AudioSource sound;
    private float ParticleSizeMultiplier;

	// Use this for initialization
	void Start () {
        arrow = GameObject.Find ("Arrow").GetComponent<Image>();
		arrow.enabled = false;
        if (WindDirection == WindOptions.UpAndDown)
        {
            direction = new Vector3(0, WindStrength, 0);
            ParticleSizeMultiplier = GetComponent<BoxCollider>().size.y / 10;
        }
        else if (WindDirection == WindOptions.LeftAndRight)
        {
            direction = new Vector3(WindStrength, 0, 0);
            ParticleSizeMultiplier = GetComponent<BoxCollider>().size.x / 10;
        }
        else if (WindDirection == WindOptions.ForwardAndBack)
        {
            direction = new Vector3(0, 0, WindStrength);
            ParticleSizeMultiplier = GetComponent<BoxCollider>().size.z / 10;
        }
        else
            return;
		WindParticles = transform.Find ("WindParticles").GetComponent<ParticleSystem>();
        WindParticles.transform.localScale *= ParticleSizeMultiplier;
		InvokeRepeating ("ChangeWindDirection", 0, DirectionDuration);
		sound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (InWindRegion && player != null)
        {
            if (WantArrowHelp && !arrow.enabled)
            {
                arrow.enabled = true;
                PickArrowDirection();
            }
            if (player.GetComponent<Rigidbody>() != null)
            {
                player.GetComponent<Rigidbody>().AddForce(direction.x * Time.deltaTime * 50, direction.y * Time.deltaTime * 50, direction.z * Time.deltaTime * 50);
                if (player.GetComponent<Rigidbody>().velocity.magnitude > MaxSpeedThroughWind)
                    player.GetComponent<Rigidbody>().AddForce(player.GetComponent<Rigidbody>().velocity.x * -10f * Time.deltaTime * 50, 0, player.GetComponent<Rigidbody>().velocity.z * -10f * Time.deltaTime * 50);
            }
            
        }
        ArrowColor[0] += 136f * Time.deltaTime * 1.4f / (255f*DirectionDuration);
        ArrowColor[1] -= 136f * Time.deltaTime * 1.4f / (255f*DirectionDuration);
        if(ArrowColor[0] >= 255)
        	ArrowColor[0] = 119;
        if(player!=null)
            arrow.color = new Color(ArrowColor[0], ArrowColor[1], ArrowColor[2], ArrowColor[3]);

    }



    void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.name == "player") {
            player = col.gameObject;
            if (WantArrowHelp){
				PickArrowDirection ();
				arrow.enabled =true;
			}
            player_control.CanJump = false;
			InWindRegion = true;
			sound.Play();
		} else {
			InWindRegion = false;
			player = null;
		}

	}

	//OnTriggerEnter was sometimes not triggering, serves to double check if player got through trigger without triggering
	void OnTriggerStay (Collider col)
	{
		if (col.gameObject.name == "player" && !InWindRegion) {
			if (WantArrowHelp){
				PickArrowDirection ();
				arrow.enabled =true;
			}
            player_control.CanJump = false;
            InWindRegion = true;
			player = col.gameObject;
		}
	}

	void OnTriggerExit (Collider col)
	{
		//CancelInvoke ("ChangeWindDirection");
		arrow.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
		arrow.enabled = false;
		InWindRegion = false;
        player_control.CanJump = true;
        player = null;
		arrow.color = new Color(ARROW_R, ARROW_G, ARROW_B, ARROW_A);
		sound.Stop();
	}

	void ChangeWindDirection()
	{
		//flip the wind direction
		direction = new Vector3(direction.x * -1, direction.y * -1, direction.z * -1);
        if (player!=null)
		    arrow.color = new Color(ARROW_R, ARROW_G, ARROW_B, ARROW_A);
		ArrowColor [0] = ARROW_R;
		ArrowColor [1] = ARROW_G;
		ArrowColor [2] = ARROW_B;
		ArrowColor [3] = ARROW_A;

		PickArrowDirection ();
	}

	void PickArrowDirection()
	{
		switch (WindDirection)
		{
		case WindOptions.UpAndDown:
			if (direction.y > 0){
				if (player != null)
					arrow.transform.rotation = Quaternion.Euler(0,0,90);	//up
				WindParticles.transform.localPosition = new Vector3(0, -14f, 0);
				WindParticles.transform.localRotation = Quaternion.Euler(-90, 0, 0);
			}
			else{
				if (player != null)
					arrow.transform.rotation = Quaternion.Euler(0,0,270);	//down
				WindParticles.transform.localPosition = new Vector3(0, 14f, 0);
				WindParticles.transform.localRotation = Quaternion.Euler(90, 0, 0);
			}
			break;
		case WindOptions.ForwardAndBack:
			if (direction.z > 0){
				if (player != null)
					arrow.transform.rotation = Quaternion.Euler(0,0,90);	//forward
				WindParticles.transform.localPosition = new Vector3(14f, 1f, 0f);
				WindParticles.transform.localRotation = Quaternion.Euler(0, -90, 0);
			}
			else{
				if (player != null)
					arrow.transform.rotation = Quaternion.Euler(0,0,270);	//down
				WindParticles.transform.localPosition = new Vector3(-14f, 1f, 0f);
				WindParticles.transform.localRotation = Quaternion.Euler(0, 90, 0);
			}
			break;
		case WindOptions.LeftAndRight:
			if (direction.x > 0){
				if (player != null)
					arrow.transform.rotation = Quaternion.Euler(0,0,0);	//right
				WindParticles.transform.localPosition = new Vector3(0.3f, -2.5f, -14f);
				WindParticles.transform.localRotation = Quaternion.Euler(0, 0, 0);
			}
			else{
				if (player != null)
					arrow.transform.rotation = Quaternion.Euler(0,0,180);	//left
				WindParticles.transform.localPosition = new Vector3(0.3f, -2.5f, 14f);
				WindParticles.transform.localRotation = Quaternion.Euler(0, 180, 0);
			}
			break;
		}
	}
}
