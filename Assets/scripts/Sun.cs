using UnityEngine;
using System.Collections;

public class Sun : Item {
    private GameObject Ray;
    private const int ADDED_TIME = 20;
	// Use this for initialization
	void Start () {
        CheckForDuplicateID();
		if (ID != null) {
			Ray = transform.Find ("SunRay").gameObject;
			InvokeRepeating ("CreateRay", 0, 0.3f);
		}
		description = "Add " + ADDED_TIME.ToString() + " seconds to the level.";
		image = Sprite.Create (Resources.Load ("sun") as Texture2D, new Rect (0, 0, 128, 128), Vector2.zero);
		Stacks = true;
		DestroyOnUse = true;
	}
	
	// Update is called once per frame
	void Update () {
        CheckForCollect();
    }

    void CreateRay()
    {
        Instantiate(Ray, transform.position, Random.rotation);
    }

    public override void UseItem()
    {
        GameObject.Find("LevelLight").GetComponent<LightTimer>().AddTime(ADDED_TIME);
    }
    
    public override void Disable()
    {
        Destroy(GetComponent<ParticleSystem>());
        Destroy(GetComponent<SphereCollider>());
        CancelInvoke("CreateRay");
    }
}
