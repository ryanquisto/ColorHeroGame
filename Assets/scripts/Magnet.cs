using UnityEngine;
using System.Collections;

public class Magnet : Item {
    private GameObject wrench;
    private GameObject dust;
    private GameObject player;
    private bool active;
	// Use this for initialization
	void Start () {
        CheckForDuplicateID();
		image = Sprite.Create (Resources.Load ("magnet_icon") as Texture2D, new Rect (0, 0, 100, 100), Vector2.zero);
		//image = Resources.Load ("potion") as Sprite;
		Stacks = false;
		DestroyOnUse = true;
		description = "Pulls the wrench and pixie dust to you";
    }
	
	// Update is called once per frame
	void Update () {
        CheckForCollect();
        if (active)
        {
            if (Vector3.Magnitude(player.transform.position - dust.transform.position) <= 2 && (wrench == null || Vector3.Magnitude(player.transform.position - wrench.transform.position) <= 4))
            {
                active = false;
                if (wrench != null)
                    wrench.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                if (Vector3.Magnitude(player.transform.position - dust.transform.position) > 2)
                    dust.transform.position = Vector3.MoveTowards(dust.transform.position, player.transform.position, 1);
                if (wrench != null && Vector3.Magnitude(player.transform.position - wrench.transform.position) > 4)
                {
                    wrench.GetComponent<Rigidbody>().useGravity = false;
                    wrench.transform.position = Vector3.MoveTowards(wrench.transform.position, player.transform.position, 1);
                }
                else if (wrench != null)
                    wrench.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    public override void UseItem()
    {
        wrench = GameObject.Find("wrench");
        dust = GameObject.Find("PixieDust");
        player = GameObject.Find("player");
        active = true;
    }
}
