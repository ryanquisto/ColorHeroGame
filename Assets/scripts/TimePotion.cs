using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimePotion : Item {
	// Use this for initialization

	private bool active;
	public Texture ActiveImage;
	private const int ActiveTime = 30/2;	//TimeScale is 1/2 normal while active
	private float TimeRemaining;
	private bool BlinkOff;
	private bool Blinking;
	void Start () {
        CheckForDuplicateID();
		image = Sprite.Create (Resources.Load ("potion") as Texture2D, new Rect (0, 0, 100, 100), Vector2.zero);
		//image = Resources.Load ("potion") as Sprite;
		ActiveImage = Resources.Load("potion_tex") as Texture;
		Stacks = true;
		DestroyOnUse = true;
		description = "Slows down time by 50% for 30 seconds";
    }

    // Update is called once per frame
    void Update () {
		CheckForCollect ();
		if (active) {
			TimeRemaining-=Time.deltaTime;
		}
        if (GetComponent<MeshRenderer>())
        {
            transform.Rotate(new Vector3(0,0.5f,1), 3);
        }
	}



	public override void UseItem(){
		active = true;
		Time.timeScale = 0.5f;
		GameObject.Find("player").GetComponent<player_control>().setSpeed(3);
		Invoke ("EndEffect", ActiveTime);
		TimeRemaining = ActiveTime;		
	}

	void EndEffect(){
		Time.timeScale = 1f;
		GameObject.Find("player").GetComponent<player_control>().resetSpeed();
		active = false;
		CancelInvoke ("Blink");
		Blinking = false;
    }

	void OnGUI(){
		if (active) {
			GUIContent content = new GUIContent();
			if (!BlinkOff)
				GUI.Label(new Rect(20, Screen.height - ActiveImage.height, 100, 100), ActiveImage);
			if (TimeRemaining <=5/2 && !Blinking){	//TimeScale is 1/2 normal while active
				InvokeRepeating("Blink",0,0.25f);
				Blinking = true;
			}
		}
	}

	void Blink(){
		BlinkOff = !BlinkOff;
	}
}
