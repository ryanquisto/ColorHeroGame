using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Book : MonoBehaviour {
    public string BookHint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            GameController.BookTextPanel.SetActive(true);
            GameController.BookTextPanel.GetComponentInChildren<Text>().text = BookHint;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            GameController.BookTextPanel.GetComponentInChildren<Text>().text = null;
            GameController.BookTextPanel.SetActive(false);
        }
    }
}
