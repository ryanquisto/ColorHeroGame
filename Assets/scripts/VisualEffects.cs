using UnityEngine;
using System.Collections;

public class VisualEffects : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void FadeObject(GameObject obj, float time=1)
    {
        //obj.GetComponent<//("albedo", new Color(obj.GetComponent<Material>().color.r, obj.GetComponent<Material>().color.g, obj.GetComponent<Material>().color.b, obj.GetComponent<Material>().color.a - Time.deltaTime / time));
    }
}
