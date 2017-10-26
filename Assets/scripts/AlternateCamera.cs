using UnityEngine;
using System.Collections;

//must switch to this camera on trigger enter, return to old camera on trigger exit, and change player control based on y angle
public class AlternateCamera : MonoBehaviour {
    Camera MainCam;
    Camera AltCam;
    // Use this for initialization
    void Start () {
        Invoke("FindCameras", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            AltCam.enabled = true;
            MainCam.enabled = false;
            if ((Mathf.Abs(AltCam.transform.eulerAngles.y + 270) % 360) < 1)         //angle is 90 degrees
                col.gameObject.GetComponent<player_control>().ChangeArrowKeys(KeyCode.LeftArrow);
            else if ((Mathf.Abs(AltCam.transform.eulerAngles.y + 90) % 360) < 1)     //angle is 270 degrees
                col.gameObject.GetComponent<player_control>().ChangeArrowKeys(KeyCode.RightArrow);
            else if (Mathf.Abs((AltCam.transform.eulerAngles.y + 180) % 360) < 1)    //angle is 180 degrees
                col.gameObject.GetComponent<player_control>().ChangeArrowKeys(KeyCode.DownArrow);
        }

    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            AltCam.enabled = false;
            MainCam.enabled = true;
            col.gameObject.GetComponent<player_control>().ChangeArrowKeys(KeyCode.UpArrow);
        }
    }

    void FindCameras()
    {
        //AltCam = Transform.FindObjectOfType<Camera>().GetComponent<Camera>();
        AltCam = transform.Find("Camera").GetComponent<Camera>();
        MainCam = Camera.main.GetComponent<Camera>();
        AltCam.enabled = false;
        MainCam.enabled = true;
    }
}
