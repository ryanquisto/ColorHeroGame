using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayPauseButton : MonoBehaviour {
    public Sprite PlayButton;
    private Sprite PauseButton;
	// Use this for initialization
	void Start () {
        PauseButton = GetComponent<Button>().GetComponent<Image>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            GetComponent<Button>().GetComponent<Image>().sprite = PauseButton;
        }
        else
        {
            Time.timeScale = 0;
            GetComponent<Button>().GetComponent<Image>().sprite = PlayButton;

        }

    }
}
