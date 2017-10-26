using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public static List<Item> CollectedItems = new List<Item>();
    public static List<string> CollectedIDs = new List<string>();
    public static bool FoundAlternatePlayer;
	// Use this for initialization
	void Start () {
        if (GameObject.FindObjectsOfType<Inventory>().Length>1)
           Destroy(this);
        UnityEngine.Object.DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
